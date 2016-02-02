using ContactosModel.Model;

namespace RepositorioAdapter.Repositorio
{
    public interface IRepositorioCanAdd<TModel> where TModel : DomainModel
    {
        TModel Add(TModel model);
    }
}