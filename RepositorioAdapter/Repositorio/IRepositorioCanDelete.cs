using System;
using System.Linq.Expressions;
using ContactosModel.Model;

namespace RepositorioAdapter.Repositorio
{
    public interface IRepositorioCanDelete<TModel> where TModel : DomainModel
    {
        int Delete(params object[] keys);
        int Delete(TModel model);
        int Delete(Expression<Func<TModel, bool>> expression);
    }
}