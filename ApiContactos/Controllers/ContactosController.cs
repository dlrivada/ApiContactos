using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Practices.Unity;

namespace ApiContactos.Controllers
{
    public class ContactosController : ApiController
    {
        [Dependency]
        public ContactoRepositorio ContactoRepositorio { get; set; }

        public ICollection<ContactoModel> Get(int id,bool amigos)
        {
            if (amigos)
                return ContactoRepositorio.GetByOrigen(id);
            return ContactoRepositorio.GetNoContactosByOrigen(id);
        }

        [ResponseType(typeof (ContactoModel))]
        public IHttpActionResult Post(ContactoModel model)
        {
            var data = ContactoRepositorio.Add(model);
            if (data == null)
                return BadRequest();
            return Ok(data);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(ContactoModel model)
        {
            var data = ContactoRepositorio.Delete(model);
            if (data <1)
                return BadRequest();
            return Ok();
        }

    }
}
