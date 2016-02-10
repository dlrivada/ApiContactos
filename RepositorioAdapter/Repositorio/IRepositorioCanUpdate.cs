using DomainModels.Base;

namespace RepositorioAdapter.Repositorio
{
    public interface IRepositorioCanUpdate<in TModel> where TModel : DomainModel
    {
        void Update(TModel model);
    }
}