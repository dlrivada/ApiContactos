using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Domain.Model.ContactAggregate;
using Infrastructure.EntityFramework;
using Microsoft.Practices.Unity;

namespace ApiContactos.Controllers
{
    [Authorize]
    public class MessagesController : ApiController
    {
        [Dependency]
        private MessageRepositoryEf MessageRepository { get; }
        [Dependency]
        private UserRepositoryEf UserRepository { get; }

        public MessagesController(MessageRepositoryEf messageRepository, UserRepositoryEf userRepository)
        {
            MessageRepository = messageRepository;
            UserRepository = userRepository;
        }

        [HttpPost]
        [ResponseType(typeof(Message))]
        public IHttpActionResult Post(User auth, Message model)
        {
            if (auth == null)
                return Unauthorized();
            if (model == null)
                return NotFound();
            // TODO: Comprobar que el usuario está autorizado y autenticado
            User user = UserRepository.Get(auth, u => u.Id == model.Id);
            if (user == null)
                return NotFound();
            // El origen y el usuario logueado son el mismo
            if (auth.Id != user.Id)
                return Unauthorized();

            try
            {
                MessageRepository.Add(auth, model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok(model);
        }

        [HttpGet]
        [ResponseType(typeof(ICollection<Message>))]
        public IHttpActionResult GetMessagesList(User auth)
        {
            if (auth == null)
                return Unauthorized();
            // TODO: Comprobar que el usuario está autorizado y autenticado
            User user = UserRepository.Get(auth, u => u.Id == auth.Id);
            if (user == null)
                return Unauthorized();

            List<Message> data = new List<Message>();
            data.AddRange(MessageRepository.Get(auth, u => u.From.Id == user.Id));
            
            return Ok(data);
        }
    }
}