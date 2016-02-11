using DomainModels.Base;

namespace Repositorio.RepositorioBase
{
    public interface IRepositorioCanDelete<in TAuth, in TModel> where TModel : DomainModel
    {
        void Delete(TAuth authentication, TModel model);
    }
}