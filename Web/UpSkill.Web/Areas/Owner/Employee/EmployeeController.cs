﻿using UpSkill.Web.Infrastructure.Services;

namespace UpSkill.Web.Areas.Owner.Employee
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using UpSkill.Services.Data.Contracts.Employee;
    using UpSkill.Web.ViewModels.Employee;
    

    using static Common.GlobalConstants.ControllerRoutesConstants;
    using static Common.GlobalConstants.ControllersResponseMessages;
    using static Common.GlobalConstants.EmployeeConstants;

    public class EmployeeController : OwnerBaseController
    {
        private readonly IEmployeesService employeesService;
        private readonly ICurrentUserService currentUser;

        public EmployeeController(IEmployeesService employeesService, ICurrentUserService currentUser)
        {
            this.employeesService = employeesService;
            this.currentUser = currentUser;
        }

        [HttpGet]
        [Route(GetAllRoute)]
        public async Task<IEnumerable<ListEmployeesViewModel>> GetAll()
            => await this.employeesService.GetAllAsync<ListEmployeesViewModel>(this.currentUser.GetUserName());

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateEmployeeViewModel model)
        {
            var result = await this.employeesService.CreateAsync(model, this.currentUser.GetId());

            if (result.Failure)
            {
                return this.BadRequest(result.Error);
            }

            return this.Ok(SuccessMessage);
        }

        [HttpDelete]
        [Route(DeleteRoute)]
        public async Task<IActionResult> DeleteAsync(string email)
        {
            var result = await this.employeesService.DeleteAsync(email);

            if (result.Failure)
            {
                return this.BadRequest(result.Error);
            }

            return this.Ok(EmployeeSuccesfullyDeleted);
        }
       
    }

}
