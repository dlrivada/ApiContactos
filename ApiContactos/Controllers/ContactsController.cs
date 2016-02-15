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
        [Dependency]
        private UserRepositoryEf UserRepository { get; }

        public ContactsController(ContactRepositoryEf contactRepository, UserRepositoryEf userRepository)
        {
            ContactRepository = contactRepository;
            UserRepository = userRepository;
        }

        [HttpGet]
        [ResponseType(typeof(ICollection<Contact>))]
        public IHttpActionResult GetContacts(User auth)
        {
            if (auth == null)
                return Unauthorized();
            // TODO: Comprobar que el usuario está autorizado y autenticado
            User user = UserRepository.Get(auth, u => u.Id == auth.Id);
            if (user == null)
                return Unauthorized();

            List<Contact> data = new List<Contact>();
            data.AddRange(ContactRepository.Get(auth, u => u.Id == user.Id));

            return Ok(data);
        }

        [HttpPost]
        [ResponseType(typeof(Contact))]
        public IHttpActionResult Post(User auth, Contact model)
        {
            if (auth == null)
                return Unauthorized();
            // TODO: Comprobar que el usuario está autorizado y autenticado
            User user = UserRepository.Get(auth, u => u.Id == auth.Id);
            if (user == null)
                return Unauthorized();

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
        public IHttpActionResult Put(User auth, Contact model)
        {
            if (auth == null)
                return Unauthorized();
            // TODO: Comprobar que el usuario está autorizado y autenticado
            User user = UserRepository.Get(auth, u => u.Id == auth.Id);
            if (user == null)
                return Unauthorized();

            Contact contactoAuth = ContactRepository.Get(auth, c => c.Id == user.Id).First();
            if (contactoAuth == null)
                return NotFound();
            // El contacto a modificar es contacto del usuario actual
            if (contactoAuth.Contacts.All(c => c.Id != model.Id))
                return NotFound();

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
        public IHttpActionResult Del(User auth, Contact model)
        {
            if (auth == null)
                return Unauthorized();
            // TODO: Comprobar que el usuario está autorizado y autenticado
            User user = UserRepository.Get(auth, u => u.Id == auth.Id);
            if (user == null)
                return Unauthorized();

            Contact contactAuth = ContactRepository.Get(auth, c => c.Id == user.Id).First();
            if (contactAuth == null)
                return NotFound();
            // El contacto a modificar es contacto del usuario actual
            if (contactAuth.Contacts.All(c => c.Id != model.Id))
                return NotFound();

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