﻿namespace UpSkill.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using UpSkill.Data.Models;
    using UpSkill.Services.Contracts.Email;
    using UpSkill.Services.Contracts.Identity;
    using UpSkill.Web.Infrastructure.Extensions.Contracts;
    using UpSkill.Web.ViewModels.Identity;

    using static Common.GlobalConstants.ControllerRoutesConstants;
    using static Common.GlobalConstants.IdentityConstants;
    using static Common.GlobalConstants.MessagesConstants;

    public class IdentityController : ApiController
    {
        private readonly IIdentityService identity;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailService emailService;
        private readonly INLogger nlog;

        public IdentityController(
            IIdentityService identity,
            UserManager<ApplicationUser> userManager,
            IEmailService emailService,
            INLogger nlog)
        {
            this.identity = identity;
            this.userManager = userManager;
            this.emailService = emailService;
            this.nlog = nlog;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(RegisterRoute)]
        public async Task<IActionResult> Register(RegisterRequestModel model)
        {
            await this.ValidateRegisterModel(model);

            if (!this.ModelState.IsValid)
            {
                this.nlog.Error(model, new Exception(this.ModelState.IsValid.ToString()));

                return this.BadRequest(this.ModelState);
            }

            var isUserRegistered = await this.identity.RegisterAsync(model);

            if (isUserRegistered.Failure)
            {
                this.nlog.Error(model, new Exception(isUserRegistered.Error));

                return this.BadRequest(isUserRegistered.Error);
            }

            await this.EmailConfirmation(model.Email);

            this.nlog.Info(model);
            return this.StatusCode(201);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(LoginRoute)]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                this.nlog.Error(model, new Exception(this.ModelState.IsValid.ToString()));

                return this.BadRequest(this.ModelState);
            }

            var embededToken = await this.identity.LoginAsync(model);

            this.Response.Cookies.Append(JWT, embededToken.Token, new CookieOptions()
            {
                HttpOnly = true,
            });

            this.nlog.Info(model);

            return this.Ok(embededToken);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(LogoutRoute)]
        public IActionResult Logout()
        {
            this.Response.Cookies.Delete(JWT);

            this.nlog.Info("Logged out successfully");

            return this.Ok(new { message = SuccessMessage });
        }

        [HttpGet]
        [Route(UserRoute)]
        public async Task<LoginResponseModel> GetCurrentUser()
        {
            var user = await this.userManager.FindByEmailAsync(this.User.FindFirstValue(ClaimTypes.Email));

            var roles = await this.userManager.GetRolesAsync(user);

            var result = new LoginResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                Role = roles[0] ?? string.Empty,
            };

            this.nlog.Info(result);

            return result;
        }

        private async Task EmailConfirmation(string email)
        {
            var user = await this.userManager.FindByEmailAsync(email);

            var origin = this.Request.Headers[HeaderOrigin];
            var host = this.Request.Host.Value;

            await this.emailService.SendEmailConfirmationAsync(origin, host, user);

            this.nlog.Info("EmailConfirmation action succeeded");
        }

        private async Task ValidateRegisterModel(RegisterRequestModel model)
        {
            if (await this.userManager.Users.AnyAsync(x => x.Email == model.Email))
            {
                this.ModelState.AddModelError(nameof(model.Email), EmailExist);
            }

            if (model.Password != model.ConfirmPassword)
            {
                this.ModelState.AddModelError(nameof(model.Password), PasswordNotMatch);
            }
        }
    }
}
