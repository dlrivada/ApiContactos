using System;
using Domain.Shared;

namespace Domain.Model.ContactAggregate
{
    public class Message : Identity, IEntity<Message>
    {
        #region Constr

        public Message(Contact from, Contact to, string body)
            : this(from, to, string.Empty, body)
        {
        }

        public Message(Contact from, Contact to, string issue, string body)
        {
            From = from;
            To = to;
            Issue = issue;
            Body = body;
            Readed = false;
            CreatedDate = DateTime.Now;
        }

        protected Message()
        {
            // Needed by Entity Framework
            Readed = false;
            CreatedDate = DateTime.Now;
        }

        #endregion

        #region Props

        public string Issue { get; private set; }
        public string Body { get; private set; }
        public bool Readed { get; private set; }
        public DateTime CreatedDate { get; private set; }

        public Contact To { get; private set; }
        public Contact From { get; private set; }

        #endregion

        #region Pub Methods

        public void MarcarComoLeido()
        {
            Readed = true;
        }

        public void MarcarComoNoLeido()
        {
            Readed = false;
        }

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
            {
                return true;
            }
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (Message)obj;
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