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
        public IHttpActionResult Get() => Ok(ContactRepository.Get(User.Identity.Name));

        [Authorize]
        [HttpPost]
        [ResponseType(typeof(Contact))]
        public IHttpActionResult Post(Contact model)
        {
            try
            {
                ContactRepository.Add(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok(model);
        }

        [Authorize]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(Contact model)
        {
            ContactRepository.Update(model);

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

        [Authorize]
        [HttpDelete]
        [ResponseType(typeof(void))]
        public IHttpActionResult Del(Contact model)
        {
            ContactRepository.Delete(model);

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