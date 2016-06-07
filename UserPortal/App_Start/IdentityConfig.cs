using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using UserPortal.Models;

namespace UserPortal
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    // *** PASS IN TYPE ARGUMENT TO BASE CLASS:
    public class ApplicationUserManager : UserManager<CodeLib.ApplicationUser, int>
    {
        // *** ADD INT TYPE ARGUMENT TO CONSTRUCTOR CALL:
        public ApplicationUserManager(IUserStore<CodeLib.ApplicationUser, int> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(
            IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            // *** PASS CUSTOM APPLICATION USER STORE AS CONSTRUCTOR ARGUMENT:
            var manager = new ApplicationUserManager(
                new CodeLib.ApplicationUserStore(context.Get<CodeLib.ApplicationDbContext>()));

            // Configure validation logic for usernames

            // *** ADD INT TYPE ARGUMENT TO METHOD CALL:
            manager.UserValidator = new UserValidator<CodeLib.ApplicationUser, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. 
            // This application uses Phone and Emails as a step of receiving a 
            // code for verifying the user You can write your own provider and plug in here.

            // *** ADD INT TYPE ARGUMENT TO METHOD CALL:
            manager.RegisterTwoFactorProvider("PhoneCode",
                new PhoneNumberTokenProvider<CodeLib.ApplicationUser, int>
                {
                    MessageFormat = "Your security code is: {0}"
                });

            // *** ADD INT TYPE ARGUMENT TO METHOD CALL:
            manager.RegisterTwoFactorProvider("EmailCode",
                new EmailTokenProvider<CodeLib.ApplicationUser, int>
                {
                    Subject = "SecurityCode",
                    BodyFormat = "Your security code is {0}"
                });

            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                // *** ADD INT TYPE ARGUMENT TO METHOD CALL:
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<CodeLib.ApplicationUser, int>(
                        dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }


    // PASS CUSTOM APPLICATION ROLE AND INT AS TYPE ARGUMENTS TO BASE:
    public class ApplicationRoleManager : RoleManager<CodeLib.ApplicationRole, int>
    {
        // PASS CUSTOM APPLICATION ROLE AND INT AS TYPE ARGUMENTS TO CONSTRUCTOR:
        public ApplicationRoleManager(IRoleStore<CodeLib.ApplicationRole, int> roleStore)
            : base(roleStore)
        {
        }

        // PASS CUSTOM APPLICATION ROLE AS TYPE ARGUMENT:
        public static ApplicationRoleManager Create(
            IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(
                new CodeLib.ApplicationRoleStore(context.Get<CodeLib.ApplicationDbContext>()));
        }
    }


    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            CodeLib.Email.EmailInfo email = new CodeLib.Email.EmailInfo
                (message.Subject, CodeLib.CommonObjects.NOTIFY_EMAIL, null, message.Destination, message.Body);
            email.Send(email);

            return Task.FromResult(0);
        }
    }


    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your sms service here to send a text message.
            return Task.FromResult(0);
        }
    }


    public class ApplicationSignInManager : SignInManager<CodeLib.ApplicationUser, int>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager) { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(CodeLib.ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
