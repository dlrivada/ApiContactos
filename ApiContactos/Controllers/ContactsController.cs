using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Domain.Model.ContactAggregate;
using Infrastructure.EntityFramework;
using Microsoft.Practices.Unity;

namespace ApiContactos.Controllers
{
    public class ContactsController : ApiController
    {
        [Dependency]
        private ContactRepositoryEf ContactRepository { get; }

        public ContactsController()
        {
            ContactRepository = new ContactRepositoryEf();
        }

        [Authorize]
        [HttpGet]
        [ResponseType(typeof(ICollection<Contact>))]
        public IHttpActionResult Get()
        {
            List<Contact> data = new List<Contact>();

            data.AddRange(ContactRepository.Get(User.Identity.Name));

            return Ok(data);
        }

        [HttpPost]
        [ResponseType(typeof(Contact))]
        public IHttpActionResult Post(Usuario auth, Contact model)
        {
            try
            {
                ContactRepository.Add(auth, model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok(model);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(Usuario auth, Contact model)
        {
            ContactRepository.Update(auth, model);

            try
            {
                ContactRepository.Save();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete]
        [ResponseType(typeof(void))]
        public IHttpActionResult Del(Usuario auth, Contact model)
        {
            ContactRepository.Delete(auth, model);

            try
            {
                ContactRepository.Save();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}