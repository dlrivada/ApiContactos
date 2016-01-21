using System.Data.Entity;
using System.Linq;
using ApiContactos.Adapter;
using ApiContactos.Models;
using ContactosModel.Model;
using RepositorioAdapter.Repositorio;

namespace ApiContactos.Repositorios
{
    public class UsuarioRepositorio : BaseRepositorioEntity<Usuario, UsuarioModel, UsuarioAdapter>
    {
        public UsuarioRepositorio(DbContext context) : base(context)
        {
        }

        public UsuarioModel Validar(string login, string password) => Get(o => o.Login == login && o.Password == password).Any() ? Get(o => o.Login == login && o.Password == password).First() : null;

        public bool IsUnico(string login) => !Get(o => o.Login == login).Any();

        public override UsuarioModel Add(UsuarioModel model) => IsUnico(model.Login) ? base.Add(model) : null;
    }
}