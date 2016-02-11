using DomainModels.Base;

namespace Repositorio.RepositorioBase
{
    public interface IRepositorioCanUpdate<in TAuth, in TModel> where TModel : DomainModel
    {
        void Update(TAuth authentication, TModel model);
    }
}