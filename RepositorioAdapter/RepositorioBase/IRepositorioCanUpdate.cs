using DomainModels.Base;

namespace Repositorio.RepositorioBase
{
    public interface IRepositorioCanUpdate<in TModel> where TModel : DomainModel
    {
        void Update(TModel model);
    }
}