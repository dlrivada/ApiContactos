using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RepositorioAdapter.Repositorio
{
    public interface IRepositorio<TEntityModel, TDtoModel, TAdapter>
    {
        TDtoModel Add(TDtoModel model);
        int Delete(params object[] keys);
        int Delete(TDtoModel model);
        int Delete(Expression<Func<TDtoModel, bool>> expression);
        int Update(TDtoModel model);
        TDtoModel Get(params object[] keys);
        ICollection<TDtoModel> Get(Expression<Func<TDtoModel, bool>> expression);
        ICollection<TDtoModel> Get();
    }
}