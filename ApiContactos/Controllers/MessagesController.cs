using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Domain.Model.ContactAggregate;
using Infrastructure.EntityFramework;
using Microsoft.Practices.Unity;

namespace ApiContactos.Controllers
{
    public class MessagesController : ApiController
    {
        [Dependency]
        public MessageRepositoryEf MessageRepository { get; }

        public MessagesController()
        {
            MessageRepository = new MessageRepositoryEf();
        }

        [Authorize]
        [HttpPost]
        [ResponseType(typeof(Message))]
        public IHttpActionResult Post(Message model)
        {
            try
            {
                MessageRepository.Add(User.Identity.Name, model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok(model);
        }

        [Authorize]
        [HttpGet]
        [ResponseType(typeof(ICollection<Message>))]
        public IHttpActionResult Get() => Ok(MessageRepository.Get(User.Identity.Name));
    }
}