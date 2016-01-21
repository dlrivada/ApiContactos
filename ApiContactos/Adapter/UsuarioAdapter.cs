using ApiContactos.Models;
using ContactosModel.Model;
using RepositorioAdapter.Adapter;

namespace ApiContactos.Adapter
{
    public class UsuarioAdapter : BaseAdapter<Usuario, UsuarioModel>
    {
        public override Usuario FromViewModel(UsuarioModel model) => new Usuario()
        {
            id = model.id,
            Foto = model.Foto,
            Login = model.Login,
            Apellidos = model.Apellidos,
            Nombre = model.Nombre,
            Password = model.Password
        };

        public override UsuarioModel FromModel(Usuario model) => new UsuarioModel()
        {
            id = model.id,
            Foto = model.Foto,
            Login = model.Login,
            Apellidos = model.Apellidos,
            Nombre = model.Nombre,
            Password = model.Password
        };
    }
}