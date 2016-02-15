using System;
using System.Web.Http;
using System.Web.Http.Description;
using Domain.Model.ContactAggregate;
using Infrastructure.EntityFramework;
using Microsoft.Practices.Unity;

namespace ApiContactos.Controllers
{
    [Authorize]
    public class UsersController : ApiController
    {
        [Dependency]
        private UserRepositoryEf UserRepository { get; }

        public UsersController(UserRepositoryEf userRepository)
        {
            UserRepository = userRepository;
        }

        [AllowAnonymous]
        [Route("Register")]
        [HttpGet]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetValid(string login, string password)
        {
            User data = UserRepository.Validar(login, password);
            if (data == null)
                return NotFound();
            return Ok(data);
        }

        [AllowAnonymous]
        [HttpPost]
        [ResponseType(typeof(User))]
        public IHttpActionResult Register(User model)
        {
            try
            {
                UserRepository.Add(model);
            }
            catch (Exception) // Podemos controlar excepción isunico
            {
                return BadRequest();
            }
            return Ok(model);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(User auth, User model)
        {
            if (auth == null)
                return Unauthorized();
            if (model == null)
                return NotFound();
            // TODO: Comprobar que el usuario está autorizado y autenticado
            // El origen y el usuario logueado son el mismo
            if (auth.Id != model.Id || auth.Login != model.Login)
                return Unauthorized(); 

            User user = UserRepository.Get(auth, u => u.Id == model.Id);
            if (user == null)
                return NotFound();
            
            UserRepository.Update(auth, user);

            try
            {
                UserRepository.Save();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
