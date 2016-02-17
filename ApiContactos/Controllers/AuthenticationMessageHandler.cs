using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Domain.Model.ContactAggregate;
using Infrastructure.EntityFramework;
using Microsoft.Practices.Unity;

namespace ApiContactos.Controllers
{
    class AuthenticationMessageHandler : DelegatingHandler
    {
        [Dependency]
        private UserRepositoryEf UserRepository { get; }

        public AuthenticationMessageHandler(UserRepositoryEf repository)
        {
            UserRepository = repository;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization == null || request.Headers.Authorization.Scheme != "Basic")
                return await base.SendAsync(request, cancellationToken);
            string encodedUserPass = request.Headers.Authorization.Parameter.Trim();
            string userPass = Encoding.Default.GetString(Convert.FromBase64String(encodedUserPass));
            string[] parts = userPass.Split(":".ToCharArray());

            //Custom code to authenticate
            if (AuthenticateIdentity(parts[0], parts[1]))
            {
                // User is Valid Create a Principal for the user
                IIdentity identity = new GenericIdentity(parts[0]);
                IPrincipal principal = new GenericPrincipal(identity, new[] { "Users", "Admin" });
                SetPrincipal(principal);
                //Thread.CurrentPrincipal = principal;
            }
            else
            {
                //Authentication failed
                HttpResponseMessage response = request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
                response.Headers.Add("WWW-Authenticate", "Basic");
                return response;
            }
            return await base.SendAsync(request, cancellationToken);
        }

        private bool AuthenticateIdentity(string login, string password) => UserRepository.Validar(login, password) != null;

        private void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
                HttpContext.Current.User = principal;
        }
    }
}
