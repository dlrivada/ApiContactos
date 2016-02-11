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
    public class ContactosController : ApiController
    {
        [Dependency]
        private ContactoRepositorio ContactoRepositorio { get; }
        [Dependency]
        private UsuarioRepositorio UsuarioRepositorio { get; }

        public ContactosController(ContactoRepositorio contactoRepositorio, UsuarioRepositorio usuarioRepositorio)
        {
            ContactoRepositorio = contactoRepositorio;
            UsuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        [ResponseType(typeof(ICollection<Contacto>))]
        public IHttpActionResult GetContactos(Usuario auth)
        {
            if (auth == null)
                return Unauthorized();
            // TODO: Comprobar que el usuario está autorizado y autenticado
            Usuario usuario = UsuarioRepositorio.Get(auth, u => u.Id == auth.Id);
            if (usuario == null)
                return Unauthorized();

            List<Contacto> data = new List<Contacto>();
            data.AddRange(ContactoRepositorio.Get(auth, u => u.Id == usuario.Id));

            return Ok(data);
        }

        [HttpPost]
        [ResponseType(typeof(Contacto))]
        public IHttpActionResult Post(Usuario auth, Contacto model)
        {
            if (auth == null)
                return Unauthorized();
            // TODO: Comprobar que el usuario está autorizado y autenticado
            Usuario usuario = UsuarioRepositorio.Get(auth, u => u.Id == auth.Id);
            if (usuario == null)
                return Unauthorized();

            try
            {
                ContactoRepositorio.Add(auth, model);
            }
            catch (Exception) 
            {
                return BadRequest();
            }
            return Ok(model);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(Usuario auth, Contacto model)
        {
            if (auth == null)
                return Unauthorized();
            // TODO: Comprobar que el usuario está autorizado y autenticado
            Usuario usuario = UsuarioRepositorio.Get(auth, u => u.Id == auth.Id);
            if (usuario == null)
                return Unauthorized();

            Contacto contactoAuth = ContactoRepositorio.Get(auth, c => c.Id == usuario.Id).First();
            if (contactoAuth == null)
                return NotFound();
            // El contacto a modificar es contacto del usuario actual
            if (contactoAuth.Contactos.All(c => c.Id != model.Id))
                return NotFound();

            ContactoRepositorio.Update(auth, model);

            try
            {
                ContactoRepositorio.Save();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete]
        [ResponseType(typeof(void))]
        public IHttpActionResult Del(Usuario auth, Contacto model)
        {
            if (auth == null)
                return Unauthorized();
            // TODO: Comprobar que el usuario está autorizado y autenticado
            Usuario usuario = UsuarioRepositorio.Get(auth, u => u.Id == auth.Id);
            if (usuario == null)
                return Unauthorized();

            Contacto contactoAuth = ContactoRepositorio.Get(auth, c => c.Id == usuario.Id).First();
            if (contactoAuth == null)
                return NotFound();
            // El contacto a modificar es contacto del usuario actual
            if (contactoAuth.Contactos.All(c => c.Id != model.Id))
                return NotFound();

            ContactoRepositorio.Delete(auth, model);

            try
            {
                ContactoRepositorio.Save();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}