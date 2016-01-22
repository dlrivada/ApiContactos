using System.Data.Entity;
using System.Linq;
using ContactosModel.Model;
using RepositorioAdapter.Repositorio;

namespace EntityFrameworkDB.Repositorios
{
    public class UsuarioRepositorio : BaseRepositorioEntity<Usuario>
    {
        public UsuarioRepositorio(DbContext context) : base(context)
        {
        }

        public Usuario Validar(string login, string password) => Get(o => o.Login == login && o.Password == password).Any() ? Get(o => o.Login == login && o.Password == password).First() : null;

        public bool IsUnico(string login) => !Get(o => o.Login == login).Any();

        public override Usuario Add(Usuario model) => IsUnico(model.Login) ? base.Add(model) : null;


    }
}