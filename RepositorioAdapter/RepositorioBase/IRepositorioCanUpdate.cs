using DomainModels.Base;

namespace Repositorio.RepositorioBase
{
    public interface IRepositorioCanUpdate<TAuth, TModel> where TModel : DomainModel
    {
        void Update(TAuth authentication, TModel model);
    }
}