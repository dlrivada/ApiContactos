using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace RepositorioAdapter.Repositorio
{
    public class BaseRepositorioEntity<TEntityModel> : IRepositorio<TEntityModel> where TEntityModel : class
    {
        protected readonly DbContext Context;

        protected BaseRepositorioEntity(DbContext context)
        {
            Context = context;
        }

        public virtual TEntityModel Add(TEntityModel model)
        {
            TEntityModel guardado = model;
            Context.Set<TEntityModel>().Add(guardado);
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
            TEntityModel data = Context.Set<TEntityModel>().Find(keys);
            Context.Set<TEntityModel>().Remove(data);
            try
            {
                return Context.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public virtual int Delete(TEntityModel model)
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

        public virtual int Delete(Expression<Func<TEntityModel, bool>> expression)
        {
            IQueryable<TEntityModel> guardar = Context.Set<TEntityModel>().Where(expression);
            Context.Set<TEntityModel>().RemoveRange(guardar);
            try
            {
                return Context.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public virtual int Update(TEntityModel model)
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

        public virtual TEntityModel Get(params object[] keys) => Context.Set<TEntityModel>().Find(keys);

        public virtual ICollection<TEntityModel> Get(Expression<Func<TEntityModel, bool>> expression) => Context.Set<TEntityModel>().Where(expression).ToList();

        public virtual ICollection<TEntityModel> Get() => Context.Set<TEntityModel>().ToList();
    }
}