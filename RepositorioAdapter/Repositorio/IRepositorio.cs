using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RepositorioAdapter.Repositorio
{
    public interface IRepositorio<TEntityModel, TViewModel>
    {
        TViewModel Add(TViewModel model);
        int Delete(params object[] keys);
        int Delete(TViewModel model);
        int Delete(Expression<Func<TEntityModel, bool>> expression);
        int Update(TViewModel model);
        TViewModel Get(params object[] keys);
        ICollection<TViewModel> Get(Expression<Func<TEntityModel, bool>> expression);
        ICollection<TViewModel> Get();
    }
}