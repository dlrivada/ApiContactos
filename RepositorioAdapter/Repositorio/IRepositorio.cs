using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RepositorioAdapter.Repositorio
{
    public interface IRepositorio<TEntityModel>
    {
        TEntityModel Add(TEntityModel model);
        int Delete(params object[] keys);
        int Delete(TEntityModel model);
        int Delete(Expression<Func<TEntityModel, bool>> expression);
        int Update(TEntityModel model);
        TEntityModel Get(params object[] keys);
        ICollection<TEntityModel> Get(Expression<Func<TEntityModel, bool>> expression);
        ICollection<TEntityModel> Get();
    }
}