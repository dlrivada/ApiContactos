using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ContactosModel.Model;

namespace RepositorioAdapter.Repositorio
{
    // Es necesario impedir consultas del tipo "Dame todos los Usuarios", hay que ordenar siempre 
    // los resultados y paginarlos para que no repliquemos grandes cantidades de la DDBB en la memoria, 
    // por seguridad y para evitar transferencias de grandes cantidades de datos por canales limitados.
    public interface IRepositorioCanRead<TModel> where TModel : DomainModel
    {
        TModel Get(params object[] keys);
        ICollection<TModel> Get(Expression<Func<TModel, bool>> expression, int pageIndex, int pageSize, out int numberOfPages);
        ICollection<TModel> Get(int pageIndex, int pageSize, out int numberOfPages);
    }
}