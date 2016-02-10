using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RepositorioAdapter.Repositorio
{
    public interface IRepositorioCanRead<TModel> where TModel : DomainModel
    {
        // TODO: Habría que implementar paginación y ordenación de datos
        ICollection<TModel> Get(Expression<Func<TModel, bool>> expression);
    }
}