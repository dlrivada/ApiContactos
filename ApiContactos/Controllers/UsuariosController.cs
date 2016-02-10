using System.Web.Http;
using System.Web.Http.Description;
using EntityFrameworkDB.Repositorios;
using Microsoft.Practices.Unity;

namespace ApiContactos.Controllers
{
    public class UsuariosController : ApiController
    {
        [Dependency]
        private UsuarioRepositorio UsuarioRepositorio { get; }

        public UsuariosController(UsuarioRepositorio usuarioRepositorio)
        {
            UsuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult GetValido(string login, string password)
        {
            Usuario data = UsuarioRepositorio.Validar(login, password);
            if (data == null)
                return NotFound();
            return Ok(data);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        public IHttpActionResult GetUnico(string login) => Ok(UsuarioRepositorio.IsUnico(login));

        [HttpPost]
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult Post(Usuario model)
        {
            Usuario data = UsuarioRepositorio.Add(model);
            if (data == null)
                return BadRequest();
            return Ok(data);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, Usuario model)
        {
            Usuario d = UsuarioRepositorio.Get(id);
            if (d == null || d.Id != model.Id)
                return NotFound();

            int data = UsuarioRepositorio.Update(model);
            if (data < 1)
                return BadRequest();
            return Ok();
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id)
        {
            int data = UsuarioRepositorio.Delete(id);
            if (data < 1)
                return BadRequest();
            return Ok();
        }
    }
}
