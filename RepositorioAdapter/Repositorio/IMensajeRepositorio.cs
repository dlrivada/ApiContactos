using System.Collections.Generic;
using ContactosModel.Model;

namespace RepositorioAdapter.Repositorio
{
    public interface IMensajeRepositorio : IRepositorioCanRead<Mensaje>, IRepositorioCanAdd<Mensaje>, IRepositorioCanDelete<Mensaje>, IRepositorioCanUpdate<Mensaje>, IRepositorio
    {
        ICollection<Mensaje> GetByDestino(int idDestino, int pageIndex, int pageSize, out int numberOfPages);
        ICollection<Mensaje> GetByOrigen(int idOrigen, int pageIndex, int pageSize, out int numberOfPages);
    }
}