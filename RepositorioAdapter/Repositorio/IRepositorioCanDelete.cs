using System;
using System.Linq.Expressions;
using ContactosModel.Model;

namespace RepositorioAdapter.Repositorio
{
    public interface IRepositorioCanDelete<TModel> where TModel : DomainModel
    {
        void Delete(TModel model);
    }
}