using DomainModels.Shared;

namespace Repositorio.RepositorioBase
{
    public interface IRepositorioCanAdd<in TAuth, in TModel> where TModel : DomainModel 
    {
        void Add(TAuth authentication, TModel model);
    }
}