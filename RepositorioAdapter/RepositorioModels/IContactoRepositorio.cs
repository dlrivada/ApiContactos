using DomainModels.Model;
using Repositorio.RepositorioBase;

namespace Repositorio.RepositorioModels
{
    public interface IContactoRepositorio : IRepositorioCanRead<Usuario, Contacto>, IRepositorioCanAdd<Usuario, Contacto>, IRepositorioCanDelete<Usuario, Contacto>, IRepositorioCanUpdate<Usuario, Contacto>, IRepositorio
    {
    }
}