using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using DemoLab.Identity;
using DemoLab.Infrastructure;
using DemoLab.Services.Identity;
using DemoLab.Services.Security;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Ninject;
using Ninject.Web.Common;

namespace DemoLab
{
    public static class DependencyConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var kernel = new StandardKernel();
            kernel.Unbind<ModelValidatorProvider>();
            RegisterDependencies(kernel);

            // Configure Web API dependency resolver.
            config.DependencyResolver = new NInjectDependencyResolver(kernel);

            // Configure MVC dependency resolver.
            DependencyResolver.SetResolver(new NInjectDependencyResolver(kernel));
        }

        private static void RegisterDependencies(IKernel kernel)
        {
            kernel.Load(AppDomain.CurrentDomain.GetAssemblies());

            kernel.Bind<ApplicationIdentityDbContext>().ToMethod(context => HttpContext.Current.GetOwinContext().Get<ApplicationIdentityDbContext>());
            kernel.Bind<IApplicationUserManager>().ToMethod(context => HttpContext.Current.GetOwinContext().Get<ApplicationUserManager>());
            kernel.Bind<IApplicationRoleManager>().ToMethod(context => HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>());
            kernel.Bind<IAuthenticationManager>().ToMethod(context => HttpContext.Current.GetOwinContext().Authentication);
            kernel.Bind<IUserRoleService>().ToMethod(context => HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>());
            kernel.Bind<IUserContextService>().To<UserContextService>().InRequestScope();
            kernel.Bind<ApplicationUserManager>().ToMethod(context => HttpContext.Current.GetOwinContext().Get<ApplicationUserManager>());
        }
    }
}
