using System.Data.Entity;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Domain.Model.ContactAggregate;
using Infrastructure.EntityFramework;
using Unity.WebApi;

namespace ApiContactos
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			UnityContainer container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<DbContext, ContactsUow>();
            container.RegisterType<IContactRepository, ContactRepositoryEf>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}