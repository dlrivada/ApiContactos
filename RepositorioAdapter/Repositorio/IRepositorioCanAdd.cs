namespace RepositorioAdapter.Repositorio
{
    public interface IRepositorioCanAdd<TModel>
    {
        TModel Add(TModel model);
    }
}