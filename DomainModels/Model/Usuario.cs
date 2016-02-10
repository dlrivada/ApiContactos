using DomainModels.Base;

namespace DomainModels.Model
{
    public class Usuario : Identity
    {
        public Usuario(string login, string password)
        {
            Login = login;
            Password = password;
        }

        protected Usuario() : base()
        {
        }

        public string Login { get; private set; }
        public string Password { get; private set; }
    }
}
