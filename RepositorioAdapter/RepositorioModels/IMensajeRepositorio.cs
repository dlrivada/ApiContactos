using DomainModels.Model;
using Repositorio.RepositorioBase;

namespace Repositorio.RepositorioModels
{
    public interface IMensajeRepositorio : IRepositorioCanRead<Mensaje>, IRepositorioCanAdd<Mensaje>, IRepositorioCanDelete<Mensaje>, IRepositorioCanUpdate<Mensaje>, IRepositorio
    {
    }
}