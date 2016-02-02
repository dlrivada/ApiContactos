namespace RepositorioAdapter.Repositorio
{
    public interface IRepositorioCanUpdate<in TModel>
    {
        int Update(TModel model);
    }
}