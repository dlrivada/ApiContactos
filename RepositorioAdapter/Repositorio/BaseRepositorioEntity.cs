using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using RepositorioAdapter.Adapter;

namespace RepositorioAdapter.Repositorio
{
    class BaseRepositorioEntity<TEntityModel, TDtoModel, TAdapter> : IRepositorio<TEntityModel, TDtoModel, TAdapter> where TAdapter : IAdapter<TEntityModel, TDtoModel>, new() where TEntityModel : class where TDtoModel : class
    {
        public TDtoModel Add(TDtoModel model)
        {
            throw new NotImplementedException();
        }

        public int Delete(params object[] keys)
        {
            throw new NotImplementedException();
        }

        public int Delete(TDtoModel model)
        {
            throw new NotImplementedException();
        }

        public int Delete(Expression<Func<TDtoModel, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public int Update(TDtoModel model)
        {
            throw new NotImplementedException();
        }

        public TDtoModel Get(params object[] keys)
        {
            throw new NotImplementedException();
        }

        public ICollection<TDtoModel> Get(Expression<Func<TDtoModel, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ICollection<TDtoModel> Get()
        {
            throw new NotImplementedException();
        }
    }
}