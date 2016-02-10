namespace RepositorioAdapter.Repositorio
{
    public interface IRepositorioCanAdd<in TModel> where TModel : DomainModel
    {
        void Add(TModel model);
    }
}