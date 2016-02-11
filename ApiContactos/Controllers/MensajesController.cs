using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using DomainModels.Model;
using EntityFrameworkDB.Repositorios;
using Microsoft.Practices.Unity;

namespace ApiContactos.Controllers
{
    [Authorize]
    public class MensajesController : ApiController
    {
        [Dependency]
        private MensajeRepositorio MensajeRepositorio { get; }
        [Dependency]
        private UsuarioRepositorio UsuarioRepositorio { get; }

        public MensajesController(MensajeRepositorio mensajeRepositorio, UsuarioRepositorio usuarioRepositorio)
        {
            MensajeRepositorio = mensajeRepositorio;
            UsuarioRepositorio = usuarioRepositorio;
        }

        [HttpPost]
        [ResponseType(typeof(Mensaje))]
        public IHttpActionResult Post(Usuario auth, Mensaje model)
        {
            if (auth == null)
                return Unauthorized();
            if (model == null)
                return NotFound();
            // TODO: Comprobar que el usuario está autorizado y autenticado
            Usuario usuario = UsuarioRepositorio.Get(auth, u => u.Id == model.Id);
            if (usuario == null)
                return NotFound();
            // El origen y el usuario logueado son el mismo
            if (auth.Id != usuario.Id)
                return Unauthorized();

            try
            {
                MensajeRepositorio.Add(auth, model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok(model);
        }

        [HttpGet]
        [ResponseType(typeof(ICollection<Mensaje>))]
        public IHttpActionResult GetListaMensajes(Usuario auth)
        {
            if (auth == null)
                return Unauthorized();
            // TODO: Comprobar que el usuario está autorizado y autenticado
            Usuario usuario = UsuarioRepositorio.Get(auth, u => u.Id == auth.Id);
            if (usuario == null)
                return Unauthorized();

            List<Mensaje> data = new List<Mensaje>();
            data.AddRange(MensajeRepositorio.Get(auth, u => u.Origen.Id == usuario.Id));
            
            return Ok(data);
        }
    }
}