using DomainModels.Model;
using Repositorio.RepositorioBase;

namespace Repositorio.RepositorioModels
{
    public interface IUsuarioRepositorio : IRepositorioCanRead<Usuario, Usuario>, IRepositorioCanUpdate<Usuario, Usuario>, IRepositorio
    {
        void Add(Usuario model);
        Usuario Validar(string login, string password);
    }
}