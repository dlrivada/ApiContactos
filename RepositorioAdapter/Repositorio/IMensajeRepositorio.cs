using DomainModels.Model;

namespace RepositorioAdapter.Repositorio
{
    public interface IMensajeRepositorio : IRepositorioCanRead<Mensaje>, IRepositorioCanAdd<Mensaje>, IRepositorioCanDelete<Mensaje>, IRepositorioCanUpdate<Mensaje>, IRepositorio
    {
    }
}