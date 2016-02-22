using System;
using System.ComponentModel.DataAnnotations;
using Domain.Shared;

namespace Domain.Model.ContactAggregate
{
    public class Message : IEntity<Message>
    {
        #region Constr

        public Message(Contact from, Contact to, string body)
            : this(from, to, string.Empty, body)
        {
        }

        public Message(Contact from, Contact to, string issue, string body) : this()
        {
            From = from;
            To = to;
            Issue = issue;
            Body = body;
        }

        protected Message()
        {
            // Needed by Entity Framework
            Readed = false;
            CreatedDate = DateTime.Now;
        }

        #endregion

        #region Props

        [Required]
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be no more than {1} characters long.")]
        [DataType(DataType.Text)]
        [Display(Name = "Issue")]
        public string Issue { get; private set; }
        [Required]
        [MaxLength]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Body")]
        public string Body { get; private set; }
        [Display(Name = "Readed")]
        public bool Readed { get; private set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; private set; }

        [Required]
        [Display(Name = "To")]
        public Contact To { get; private set; }
        [Required]
        [Display(Name = "From")]
        public Contact From { get; private set; }

        #endregion

        #region Pub Methods

        public void MarcarComoLeido() => Readed = true;

        public void MarcarComoNoLeido() => Readed = false;

        #endregion

        #region IEntity<Mensaje> Members

        /// <summary>
        /// Entities compare by identity, not by attributes.
        /// </summary>
        /// <param name="other">The other entity.</param>
        /// <returns>true if the identities are the same, regardles of other attributes.</returns>
        public virtual bool SameIdentityAs(Message other)
        {
            return other != null && Id.Equals(other.Id);
        }

        #endregion

        #region Object's override

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null || GetType() != obj.GetType())
                return false;

            Message other = (Message)obj;
            return SameIdentityAs(other);
        }

        public override int GetHashCode() => Id.GetHashCode();


        public override string ToString() => Id.ToString();

        #endregion

    }
}