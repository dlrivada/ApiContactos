using System.ComponentModel.DataAnnotations;
using Domain.Shared;

namespace Domain.Model.ContactAggregate
{
    public class Usuario : Identity, IEntity<Usuario>
    {
        #region Constr

        public Usuario(string login, string password, string confirmPassword)
        {
            Login = login;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        protected Usuario()
        {
            // Needed by Entity Framework
        }

        #endregion

        #region Props

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Login")]
        public string Login { get; private set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; private set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        #endregion

        #region IEntity<Usuario> Members

        /// <summary>
        /// Entities compare by identity, not by attributes.
        /// </summary>
        /// <param name="other">The other entity.</param>
        /// <returns>true if the identities are the same, regardles of other attributes.</returns>
        public virtual bool SameIdentityAs(Usuario other) => other != null && Id.Equals(other.Id);

        #endregion

        #region Object's override

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;

            return SameIdentityAs((Usuario)obj);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => Id.ToString();

        #endregion
    }
}
