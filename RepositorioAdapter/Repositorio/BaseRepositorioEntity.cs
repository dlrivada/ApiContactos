using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace RepositorioAdapter.Repositorio
{
    // Este repositorio usa el tipo DbContext que pertenece al EntityFramework, 
    // por lo que está fuertemente acoplado a la persitencia concreta que se ha elegido
    // Para desacoplarlo de la persistencia implementarlo en 
    // UsuarioRepositorio, prescindir de Generics, y usar Unidades de Trabajo
    // TODO: implementar una Unidad de trabajo
    // TODO: prescindir de Generics
    // TODO: Usar Unidad de Trabajo
    public class BaseRepositorioEntity<TModel> : IRepositorio<TModel> where TModel : class
    {
        // Contexto de conexión y almacén de instacias del modelo
        // Ojo! Cuidado con replicar (o multiplicar) toda la Base de Datos en la Memoria.
        // Además es necesario impedir al usuario del repositorio que instacie el Contexto para
        // para tener nosotros siempre el control de la memoria consumida y del estado de la conexión
        // para eso hay que usar IoC 
        // Para acceder al almacen de instacias usar el DbSet<> del Context
        protected readonly DbContext Context;

        protected BaseRepositorioEntity(DbContext context)
        {
            Context = context;
        }

        public virtual TModel Add(TModel model)
        {
            TModel guardado = model;
            Context.Set<TModel>().Add(guardado);
            try
            {
                Context.SaveChanges();
                return guardado;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual int Delete(params object[] keys)
        {
            TModel data = Context.Set<TModel>().Find(keys);
            Context.Set<TModel>().Remove(data);
            try
            {
                return Context.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public virtual int Delete(TModel model)
        {
            Context.Entry(model).State = EntityState.Deleted;
            try
            {
                return Context.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public virtual int Delete(Expression<Func<TModel, bool>> expression)
        {
            IQueryable<TModel> guardar = Context.Set<TModel>().Where(expression);
            Context.Set<TModel>().RemoveRange(guardar);
            try
            {
                return Context.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public virtual int Update(TModel model)
        {
            Context.Entry(model).State = EntityState.Modified;
            try
            {
                return Context.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public virtual TModel Get(params object[] keys) => Context.Set<TModel>().Find(keys);
        public virtual ICollection<TModel> Get(Expression<Func<TModel, bool>> expression) => Context.Set<TModel>().Where(expression).ToList();
        public virtual ICollection<TModel> Get() => Context.Set<TModel>().ToList();
    }
}