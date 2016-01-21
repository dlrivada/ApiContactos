using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using RepositorioAdapter.Adapter;

namespace RepositorioAdapter.Repositorio
{
    public class BaseRepositorioEntity<TEntityModel, TViewModel, TAdapter> : IRepositorio<TEntityModel, TViewModel> where TAdapter : IAdapter<TEntityModel, TViewModel>, new() where TEntityModel : class where TViewModel : class
    {
        protected DbContext Context;
        private TAdapter _adapter;

        protected TAdapter Adapter
        {
            get
            {
                if (object.Equals(_adapter, default(TAdapter)))
                    _adapter = new TAdapter();
                return _adapter;
            }
        }

        public BaseRepositorioEntity(DbContext context)
        {
            Context = context;
        }

        public virtual TViewModel Add(TViewModel model)
        {
            TEntityModel guardado = Adapter.FromViewModel(model);
            Context.Set<TEntityModel>().Add(guardado);
            try
            {
                Context.SaveChanges();
                return Adapter.FromModel(guardado);
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

        public virtual int Delete(TViewModel model)
        {
            TEntityModel guardar = Adapter.FromViewModel(model);
            Context.Entry(guardar).State = EntityState.Deleted;
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

        public virtual int Update(TViewModel model)
        {
            TEntityModel guardar = Adapter.FromViewModel(model);
            Context.Entry(guardar).State = EntityState.Modified;
            try
            {
                return Context.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public virtual TViewModel Get(params object[] keys) => Adapter.FromModel(Context.Set<TEntityModel>().Find(keys));

        public virtual ICollection<TViewModel> Get(Expression<Func<TEntityModel, bool>> expression) => Adapter.FromModel(Context.Set<TEntityModel>().Where(expression).ToList());

        public virtual ICollection<TViewModel> Get() => Adapter.FromModel(Context.Set<TEntityModel>().ToList());
    }
}