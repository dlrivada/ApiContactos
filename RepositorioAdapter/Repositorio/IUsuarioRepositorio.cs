using System;
using ContactosModel.Model;

namespace RepositorioAdapter.Repositorio
{
    public interface IUsuarioRepositorio : IRepositorioCanRead<Usuario>, IRepositorioCanAdd<Usuario>, IRepositorioCanDelete<Usuario>, IRepositorioCanUpdate<Usuario>, IRepositorio<Usuario>, IDisposable
    {
        Usuario Validar(string login, string password);
        bool IsUnico(string login);
    }
}