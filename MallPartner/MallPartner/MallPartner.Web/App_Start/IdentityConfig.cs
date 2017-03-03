using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using StMallB.Web.Models;
using StMallB.Entity;
using StMallB.Service;

namespace StMallB.Web
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<JoinBusinessUser, long>
    {
        public ApplicationUserManager(IUserStore<JoinBusinessUser, long> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            return new ApplicationUserManager(new UserStore()
            {
                //PasswordHasher = new MyPasswordHasher()
            });
        }


        //public ApplicationUserManager(IUserStore<ApplicationUser, long> store)
        //    : base(store)
        //{
        //}

        //public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        //{
        //    var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
        //    // Configure validation logic for usernames
        //    manager.UserValidator = new UserValidator<ApplicationUser>(manager)
        //    {
        //        AllowOnlyAlphanumericUserNames = false,
        //        RequireUniqueEmail = true
        //    };

        //    // Configure validation logic for passwords
        //    manager.PasswordValidator = new PasswordValidator
        //    {
        //        RequiredLength = 6,
        //        RequireNonLetterOrDigit = true,
        //        RequireDigit = true,
        //        RequireLowercase = true,
        //        RequireUppercase = true,
        //    };

        //    // Configure user lockout defaults
        //    manager.UserLockoutEnabledByDefault = true;
        //    manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
        //    manager.MaxFailedAccessAttemptsBeforeLockout = 5;

        //    // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
        //    // You can write your own provider and plug it in here.
        //    manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
        //    {
        //        MessageFormat = "Your security code is {0}"
        //    });
        //    manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
        //    {
        //        Subject = "Security Code",
        //        BodyFormat = "Your security code is {0}"
        //    });
        //    manager.EmailService = new EmailService();
        //    manager.SmsService = new SmsService();
        //    var dataProtectionProvider = options.DataProtectionProvider;
        //    if (dataProtectionProvider != null)
        //    {
        //        manager.UserTokenProvider =
        //            new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
        //    }
        //    return manager;
        //}
    }

    // Configure the application sign-in manager which is used in this application.
    //public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    //{
    //    public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
    //        : base(userManager, authenticationManager)
    //    {
    //    }

    //    public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
    //    {
    //        return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
    //    }

    //    public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
    //    {
    //        return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
    //    }
    //}

    public class ApplicationSignInManager : SignInManager<JoinBusinessUser, long>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        //public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        //{
        //    return user.GenerateUserIdentityAsync((JoinBusiness)UserManager);
        //}

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }

    //public class UserStore : IUserStore<JoinBusinessUser, long>, IUserLockoutStore<JoinBusinessUser, long>, IUserPasswordStore<JoinBusinessUser, long>, IUserTwoFactorStore<JoinBusinessUser, long>
    //{
    //    private IJoinBusinessService joinBusinessService;
    //    public UserStore(IJoinBusinessService joinBusinessService)
    //    {
    //        this.joinBusinessService = joinBusinessService;
    //    }

    //    public UserStore()
    //    {
    //        // TODO: Complete member initialization
    //    }

    //    //public UserStore(WXDBContexnt dbContext)
    //    //{
    //    //    this.dbContext = dbContext;
    //    //}
    //    //WXDBContexnt dbContext;

    //    //public async Task<WXUser> FindByIdAsync(Guid userId)
    //    //{
    //    //    var user = await dbContext.WXUser.FindAsync(userId);
    //    //    return user;
    //    //}

    //    //public async Task<WXUser> FindByNameAsync(string userName)
    //    //{
    //    //    return dbContext.WXUser.Where(p => p.LoginId == userName).FirstOrDefaultAsync();
    //    //}
    //    public Task ResetAccessFailedCountAsync(JoinBusinessUser user)
    //    {
    //        return Task.FromResult(false);
    //    }
    //    public Task<bool> GetLockoutEnabledAsync(JoinBusinessUser user)
    //    {
    //        return Task.FromResult(false);
    //    }
    //    public Task<string> GetPasswordHashAsync(JoinBusinessUser user)
    //    {
    //        return Task.FromResult(user.BusPassword);
    //    }
    //    public Task<bool> GetTwoFactorEnabledAsync(JoinBusinessUser user)
    //    {
    //        return Task.FromResult(false);
    //    }

    //    public Task CreateAsync(JoinBusinessUser user)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task DeleteAsync(JoinBusinessUser user)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<JoinBusinessUser> FindByIdAsync(long userId)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<JoinBusinessUser> FindByNameAsync(string userName)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task UpdateAsync(JoinBusinessUser user)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Dispose()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<int> GetAccessFailedCountAsync(JoinBusinessUser user)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<DateTimeOffset> GetLockoutEndDateAsync(JoinBusinessUser user)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<int> IncrementAccessFailedCountAsync(JoinBusinessUser user)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task SetLockoutEnabledAsync(JoinBusinessUser user, bool enabled)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task SetLockoutEndDateAsync(JoinBusinessUser user, DateTimeOffset lockoutEnd)
    //    {
    //        throw new NotImplementedException();
    //    }


    //    public Task<bool> HasPasswordAsync(JoinBusinessUser user)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task SetPasswordHashAsync(JoinBusinessUser user, string passwordHash)
    //    {
    //        throw new NotImplementedException();
    //    }


    //    public Task SetTwoFactorEnabledAsync(JoinBusinessUser user, bool enabled)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
