using System.Data.Entity;
using Microsoft.Practices.Unity;
using System.Web.Http;
using EntityFrameworkDB;
using EntityFrameworkDB.Repositorios;
using Repositorio.RepositorioModels;
using Unity.WebApi;

namespace ApiContactos
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<DbContext, Model1>();
            container.RegisterType<IUsuarioRepositorio, UsuarioRepositorio>();
            container.RegisterType<IMensajeRepositorio, MensajeRepositorio>();
            container.RegisterType<IContactoRepositorio, ContactoRepositorio>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}