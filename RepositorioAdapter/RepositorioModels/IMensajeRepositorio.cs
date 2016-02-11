using DomainModels.Model;
using Repositorio.RepositorioBase;

namespace Repositorio.RepositorioModels
{
    public interface IMensajeRepositorio : IRepositorioCanRead<Usuario, Mensaje>, IRepositorioCanAdd<Usuario, Mensaje>, IRepositorio
    {
    }
}