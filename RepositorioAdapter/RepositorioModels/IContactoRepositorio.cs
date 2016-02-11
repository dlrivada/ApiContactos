using DomainModels.Model;
using Repositorio.RepositorioBase;

namespace Repositorio.RepositorioModels
{
    public interface IContactoRepositorio : IRepositorioCanRead<Contacto>, IRepositorioCanAdd<Contacto>, IRepositorioCanDelete<Contacto>, IRepositorioCanUpdate<Contacto>, IRepositorio
    {
    }
}