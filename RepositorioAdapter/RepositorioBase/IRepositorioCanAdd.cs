using DomainModels.Base;

namespace Repositorio.RepositorioBase
{
    public interface IRepositorioCanAdd<in TModel> where TModel : DomainModel
    {
        void Add(TModel model);
    }
}