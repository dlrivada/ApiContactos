using Domain.Shared;

namespace Domain.Model.ContactAggregate
{
    public class User : Identity, IEntity<User>
    {
        #region Constr

        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }

        protected User() : base()
        {
            // Needed by Entity Framework
        }

        #endregion

        #region Props

        public string Login { get; private set; }
        public string Password { get; private set; }

        #endregion

        #region IEntity<Usuario> Members

        /// <summary>
        /// Entities compare by identity, not by attributes.
        /// </summary>
        /// <param name="other">The other entity.</param>
        /// <returns>true if the identities are the same, regardles of other attributes.</returns>
        public virtual bool SameIdentityAs(User other)
        {
            return other != null && Id.Equals(other.Id);
        }

        #endregion

        #region Object's override

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (User)obj;
            return SameIdentityAs(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }


        public override string ToString()
        {
            return Id.ToString();
        }

        #endregion
    }
}
