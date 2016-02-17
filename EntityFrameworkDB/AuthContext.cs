using Microsoft.AspNet.Identity.EntityFramework;

namespace Infrastructure.EntityFramework
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {

        }
    }
}

