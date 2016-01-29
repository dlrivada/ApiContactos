using ContactosModel.Model;

namespace RepositorioAdapter.Repositorio
{
    public interface IRepositorioCanAdd<TModel> where TModel : DomainModel
    {
        void Add(TModel model);
    }
}