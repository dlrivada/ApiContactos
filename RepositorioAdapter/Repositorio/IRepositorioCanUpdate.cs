using ContactosModel.Model;
using RepositorioAdapter.UnitOfWork;

namespace RepositorioAdapter.Repositorio
{
    public interface IRepositorioCanUpdate<in TModel> where TModel : DomainModel
    {
        void Attach(TModel entity);
        void Attach(TModel entity, EntityStatus status);
    }
}