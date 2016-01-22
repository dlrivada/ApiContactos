using System.Data.Entity;
using Microsoft.Practices.Unity;
using System.Web.Http;
using EntityFrameworkDB;
using EntityFrameworkDB.Repositorios;
using Unity.WebApi;

namespace ApiContactos
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            using (var container = new UnityContainer())
            {
                container.RegisterType<DbContext, Model1>();
                container.RegisterType<UsuarioRepositorio>();
                container.RegisterType<MensajeRepositorio>();
                GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            }
        }
    }
}