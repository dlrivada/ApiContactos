using DomainModels.Base;

namespace RepositorioAdapter.Repositorio
{
    public interface IRepositorioCanDelete<TModel> where TModel : DomainModel
    {
        void Delete(TModel model);
    }
}