using DomainModels.Base;

namespace Repositorio.RepositorioBase
{
    public interface IRepositorioCanAdd<TAuth, TModel> where TModel : DomainModel 
    {
        void Add(TAuth authentication, TModel model);
    }
}