using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using DomainModels.Model;
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

        [HttpPost]
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult Post(Usuario model)
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
        public IHttpActionResult Put(int id, Usuario model)
        {
            Usuario usuario = UsuarioRepositorio.Get(u => u.Id == id).First();
            if (usuario == null || usuario.Id != model.Id)
                return NotFound();

            UsuarioRepositorio.Update(model);

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

        [HttpDelete]
        [ResponseType(typeof(void))]
        public IHttpActionResult Del(int id)
        {
            Usuario usuario = UsuarioRepositorio.Get(u => u.Id == id).First();
            if (usuario == null)
                return NotFound();

            UsuarioRepositorio.Delete(usuario);

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
