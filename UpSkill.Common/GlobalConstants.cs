namespace UpSkill.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "UpSkill";

        public const string AdministratorRoleName = "Administrator";

        public const string AppSettingJson = "appsettings.json";

        public const string DefaultConnection = "DefaultConnection";

        public const string ApplicationSettings = "ApplicationSettings";

        public const string Unauthorized = "Unauthorized";

        public class AutoMapperConstants
        {
            public const string ReflectionProfile = "ReflectionProfile";
        }

        public class SwaggerConstants
        {
            public const string UpSkillAPI = "UpSkill API";
            public const string V1 = "v1";
            public const string SwaggerHttpPath = "/swagger/v1/swagger.json";
        }

        public class IdentityConstants
        {
            public const string JWT = "jwt";
            public const string Identity = "identity";

            public const string UserNotFound = "There is no such user";
            public const string IncorrectEmailOrPassword = "Email or password is incorrect.";
            public const string EmailExist = "There is such exist user with this email.";
            public const string UsernameExist = "There is such exist user with this username.";
            public const string PasswordNotMatch = "Password and confirm password must be the same.";
            public const string ConfirmEmail = "Please confirm your account. From the email we sent to you.";
        }

        public class AccountConstants
        {
            public const string WrongOldPassword = "Old password is invalid.";
            public const string DifferentPasswords = "New password and confirm new password must be the same.";
        }

        public class AccountConstants
        {
            public const string WrongOldPassword = "Old password is invalid.";
            public const string DifferentPasswords = "New password and confirm new password must be the same.";
        }

        public class ControllerRoutesConstants
        {
            public const string HeaderOrigin = "origin";

            public const string LoginRoute = "login";
            public const string RegisterRoute = "register";
            public const string LogoutRoute = "logout";
            public const string UserRoute = "user";

            public const string VerifyEmailRoute = "verifyEmail";
            public const string ResendEmailConfirmationLinkRoute = "resendEmailConfirmationLink";

            public const string ChangePasswordRoute = "changePassword";
        }

        public class MessagesConstants
        {
            public const string SuccessMessage = "Success";
            public const string IncorrectEmail = "Your email is incorrect.";
            public const string EmailConfirmed = "Email confirmed - you can now login.";
            public const string EmailsDoNotMatch = "This is not the email you registered with.";
        }

        public class EmailSenderConstants
        {
            public const string SendGridApiKey = "SendGrid:ApiKey";

            public const string EmailControllerName = "email";
            public const string FromEmail = "middnight_man@protonmail.com";
            public const string FromName = "middnight_man";
            public const string EmailSubject = "Verify Email";
            public const string HtmlContent = "<p>Verify your email:</p><p><a href='{0}'>CLICK here</a></p>";
            public const string VerifyUrl = "https:/{0}/{1}/{2}/{3}?token={4}&email={5}";
        }
    }
}
