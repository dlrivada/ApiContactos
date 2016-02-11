using System;
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

        public ContactosController(ContactoRepositorio contactoRepositorio)
        {
            ContactoRepositorio = contactoRepositorio;
        }

        [HttpPost]
        [ResponseType(typeof(Contacto))]
        public IHttpActionResult Post(Contacto model)
        {
            try
            {
                ContactoRepositorio.Add(model);
            }
            catch (Exception) // Podemos controlar excepción isunico
            {
                return BadRequest();
            }
            return Ok(model);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, Contacto model)
        {
            Contacto contacto = ContactoRepositorio.Get(u => u.Id == id).First();
            if (contacto == null || contacto.Id != model.Id)
                return NotFound();

            ContactoRepositorio.Update(model);

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
        public IHttpActionResult Del(int id)
        {
            Contacto contacto = ContactoRepositorio.Get(u => u.Id == id).First();
            if (contacto == null)
                return NotFound();

            ContactoRepositorio.Delete(contacto);

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