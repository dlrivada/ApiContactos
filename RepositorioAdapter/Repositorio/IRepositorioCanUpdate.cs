using ContactosModel.Model;

namespace RepositorioAdapter.Repositorio
{
    public interface IRepositorioCanUpdate<in TModel> where TModel : DomainModel
    {
        int Update(TModel model);
    }
}