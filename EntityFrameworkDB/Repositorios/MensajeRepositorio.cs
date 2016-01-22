using System.Data.Entity;
using ContactosModel.Model;
using RepositorioAdapter.Repositorio;

namespace EntityFrameworkDB.Repositorios
{
    public class MensajeRepositorio : BaseRepositorioEntity<Mensaje>
    {
        public MensajeRepositorio(DbContext context) : base(context)
        {
        }
    }
}