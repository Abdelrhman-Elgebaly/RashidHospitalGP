using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Hospital.DAL;
using RashidHospital.Controllers;
using Unity.Injection;

namespace RashidHospital
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            // register all your components with the container here
            // it is NOT necessary to register your controllers
        //    container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            container.RegisterType<Model1>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}