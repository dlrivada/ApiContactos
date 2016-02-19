using System.Threading.Tasks;
using System.Web.Http;
using Domain.Model.ContactAggregate;
using Infrastructure.EntityFramework;
using Microsoft.AspNet.Identity;

namespace ApiContactos.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private AuthRepository Repo { get; }
        private ContactRepositoryEf ContactRepository { get; }

        public AccountController()
        {
            Repo = new AuthRepository();
            ContactRepository = new ContactRepositoryEf();
        }
        
        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(Usuario userModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            IdentityResult result = await Repo.RegisterUser(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            ContactRepository.Add(new Contact(userModel.Login, userModel.Password));

            return errorResult ?? Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Repo.Dispose();

            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
                return InternalServerError();

            if (result.Succeeded)
                return null;

            if (result.Errors != null)
                foreach (string error in result.Errors)
                    ModelState.AddModelError("", error);

            return ModelState.IsValid ? (IHttpActionResult) BadRequest() : BadRequest(ModelState);
        }
    }
}