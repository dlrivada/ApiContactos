using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using DomainModels.Model;
using EntityFrameworkDB.Repositorios;
using Microsoft.Practices.Unity;

namespace ApiContactos.Controllers
{
    public class MensajesController : ApiController
    {
        [Dependency]
        private MensajeRepositorio MensajeRepositorio { get; }

        public MensajesController(MensajeRepositorio mensajeRepositorio)
        {
            MensajeRepositorio = mensajeRepositorio;
        }

        [HttpPost]
        [ResponseType(typeof(Mensaje))]
        public IHttpActionResult Post(Usuario usuario, Mensaje model)
        {
            try
            {
                MensajeRepositorio.Add(model);
            }
            catch (Exception) // Podemos controlar excepción isunico
            {
                return BadRequest();
            }
            return Ok(model);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, Mensaje model)
        {
            Mensaje mensaje = MensajeRepositorio.Get(u => u.Id == id).First();
            if (mensaje == null || mensaje.Id != model.Id)
                return NotFound();

            MensajeRepositorio.Update(model);

            try
            {
                MensajeRepositorio.Save();
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
            Mensaje mensaje = MensajeRepositorio.Get(u => u.Id == id).First();
            if (mensaje == null)
                return NotFound();

            MensajeRepositorio.Delete(mensaje);

            try
            {
                MensajeRepositorio.Save();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}