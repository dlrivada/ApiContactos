using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using DomainModels.Model;
using EntityFrameworkDB.Repositorios;
using Microsoft.Practices.Unity;

namespace ApiContactos.Controllers
{
    [Authorize]
    public class UsuariosController : ApiController
    {
        [Dependency]
        private UsuarioRepositorio UsuarioRepositorio { get; }

        public UsuariosController(UsuarioRepositorio usuarioRepositorio)
        {
            UsuarioRepositorio = usuarioRepositorio;
        }

        [AllowAnonymous]
        [Route("Register")]
        [HttpGet]
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult GetValido(string login, string password)
        {
            Usuario data = UsuarioRepositorio.Validar(login, password);
            if (data == null)
                return NotFound();
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult Register(Usuario model)
        {
            try
            {
                UsuarioRepositorio.Add(model);
            }
            catch (Exception) // Podemos controlar excepción isunico
            {
                return BadRequest();
            }
            return Ok(model);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(Usuario auth, Usuario model)
        {
            if (auth == null)
                return Unauthorized();
            if (model == null)
                return NotFound();
            // TODO: Comprobar que el usuario está autorizado y autenticado
            // El origen y el usuario logueado son el mismo
            if (auth.Id != model.Id || auth.Login != model.Login)
                return Unauthorized(); 

            Usuario usuario = UsuarioRepositorio.Get(auth, u => u.Id == model.Id);
            if (usuario == null)
                return NotFound();
            
            UsuarioRepositorio.Update(auth, usuario);

            try
            {
                UsuarioRepositorio.Save();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
