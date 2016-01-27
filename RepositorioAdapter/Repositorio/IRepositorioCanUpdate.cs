using System.Data.Entity;

namespace RepositorioAdapter.Repositorio
{
    public interface IRepositorioCanUpdate<in TModel>
    {
        int Update(TModel model);
    }
    public interface IRepositorio<in TModel>
    {
        DbContext Context { get; }
    }
}