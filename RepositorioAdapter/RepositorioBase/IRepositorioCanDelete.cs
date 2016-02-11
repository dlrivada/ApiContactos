using DomainModels.Base;

namespace Repositorio.RepositorioBase
{
    public interface IRepositorioCanDelete<TModel> where TModel : DomainModel
    {
        void Delete(TModel model);
    }
}