using System.Threading.Tasks;
using System.Web.Http;
using Domain.Model.ContactAggregate;
using Infrastructure.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;

namespace ApiContactos.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private AuthRepository Repo { get; }

        public AccountController()
        {
            Repo = new AuthRepository();
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