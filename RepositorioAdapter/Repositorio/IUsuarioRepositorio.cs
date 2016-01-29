using System.Collections.Generic;
using ContactosModel.Model;

namespace RepositorioAdapter.Repositorio
{
    public interface IUsuarioRepositorio : IRepositorioCanRead<Usuario>, IRepositorioCanAdd<Usuario>, IRepositorioCanDelete<Usuario>, IRepositorioCanUpdate<Usuario>, IRepositorio
    {
        bool IsUnico(string login);
        ICollection<Usuario> GetFullUser();
        Usuario Validar(string login, string password);
    }
}