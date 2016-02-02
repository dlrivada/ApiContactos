using ContactosModel.Model;

namespace RepositorioAdapter.Repositorio
{
    public interface IRepositorioCanDelete<TModel> where TModel : DomainModel
    {
        int Delete(TModel model);
    }
}