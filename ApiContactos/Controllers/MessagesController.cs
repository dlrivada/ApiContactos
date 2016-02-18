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
        private MessageRepositoryEf MessageRepository { get; }

        public MessagesController(MessageRepositoryEf messageRepository)
        {
            MessageRepository = messageRepository;
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
        public IHttpActionResult GetMessagesList()
        {
            List<Message> data = new List<Message>();
            data.AddRange(MessageRepository.Get(User.Identity.Name));
            
            return Ok(data);
        }
    }
}