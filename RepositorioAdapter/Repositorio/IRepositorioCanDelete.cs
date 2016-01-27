using System;
using System.Linq.Expressions;

namespace RepositorioAdapter.Repositorio
{
    public interface IRepositorioCanDelete<TModel>
    {
        int Delete(params object[] keys);
        int Delete(TModel model);
        int Delete(Expression<Func<TModel, bool>> expression);
    }
}