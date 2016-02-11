using DomainModels.Model;
using Repositorio.RepositorioBase;

namespace Repositorio.RepositorioModels
{
    public interface IUsuarioRepositorio : IRepositorioCanRead<Usuario>, IRepositorioCanAdd<Usuario>, IRepositorioCanDelete<Usuario>, IRepositorioCanUpdate<Usuario>, IRepositorio
    {
        Usuario Validar(string login, string password);
    }
}