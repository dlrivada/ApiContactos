using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using ContactosModel.Model;
using EntityFrameworkDB.Repositorios;
using Microsoft.Practices.Unity;

namespace ApiContactos.Controllers
{
    public class MensajeController : ApiController
    {
        [Dependency]
        private MensajeRepositorio MensajeRepositorio { get; }

        public MensajeController(MensajeRepositorio mensajeRepositorio)
        {
            MensajeRepositorio = mensajeRepositorio;
        }

        public ICollection<Mensaje> Get(int id) => MensajeRepositorio.GetByDestino(id);

        public ICollection<Mensaje> Get(int id,bool enviado) => MensajeRepositorio.GetByOrigen(id);

        [ResponseType(typeof (Mensaje))]
        public IHttpActionResult Post(Mensaje model)
        {
            Mensaje data= MensajeRepositorio.Add(model);
            if (data == null)
                return BadRequest();
            return Ok(data);
        }

        [ResponseType(typeof (void))]
        public IHttpActionResult Put(Mensaje model)
        {
            int data = MensajeRepositorio.Update(model);
            if (data < 1)
                return BadRequest();
            return Ok();
        }
    }
}
