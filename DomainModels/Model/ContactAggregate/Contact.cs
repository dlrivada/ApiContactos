using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain.Model.ContactAggregate
{
    public class Contact : Usuario
    {
        #region Props

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 3)]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; private set; }
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 3)]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; private set; }
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Photo")]
        public string Photo { get; private set; }

        public ICollection<Contact> Contacts { get; private set; }
        public ICollection<Contact> ContactsFrom { get; private set; }
        public ICollection<Message> MessagesSended { get; private set; }
        public ICollection<Message> MessagesReceived { get; private set; }

        #endregion

        #region Constr

        public Contact(string login, string password) : this(login, password, string.Empty)
        {
        }

        public Contact(string login, string password, string firstName) : this(login, password, firstName, string.Empty)
        {
        }

        public Contact(string login, string password, string firstName, string lastName)
            : this(login, password, firstName, lastName, string.Empty)
        {
        }

        public Contact(string login, string password, string firstName, string lastName, string photo)
            : base(login, password, password)
        {
            FirstName = firstName;
            LastName = lastName;
            Photo = photo;

            if (MessagesSended == null)
                MessagesSended = new List<Message>();

            if (MessagesReceived == null)
                MessagesReceived = new List<Message>();

            if (Contacts == null)
                Contacts = new List<Contact>();

            if (ContactsFrom == null)
                ContactsFrom = new List<Contact>();
        }

        protected Contact()
        {
            // Needed by Entity Framework

            if (MessagesSended == null)
                MessagesSended = new List<Message>();

            if (MessagesReceived == null)
                MessagesReceived = new List<Message>();

            if (Contacts == null)
                Contacts = new List<Contact>();

            if (ContactsFrom == null)
                ContactsFrom = new List<Contact>();
        }

        #endregion

        #region Pub Methods

        public void SetContactoDetails(string nombre, string apellidos, string foto)
        {
            FirstName = nombre;
            LastName = apellidos;
            Photo = foto;
        }

        public void AddContacto(Contact contacto)
        {
            Contacts.Add(contacto);
            contacto.ContactsFrom.Add(this);
        }

        public void RemoveContacto(Contact contacto)
        {
            Contacts.Remove(contacto);
            contacto.ContactsFrom.Remove(this);
        }

        public void AddMensaje(Contact destino, string asunto, string contenido)
        {
            // Sólo se puede escribir a los propios contactos
            if (Contacts.All(c => c.Login != destino.Login) || contenido == string.Empty)
                return;

            Message nuevoMensaje = new Message(this, destino, asunto, contenido);
            MessagesSended.Add(nuevoMensaje);
            destino.MessagesReceived.Add(nuevoMensaje);
        }

        public void AddMensaje(Contact destino, string contenido) => AddMensaje(destino, string.Empty, contenido);

        public void RemoveMensaje(Message mensaje)
        {
            mensaje.To.MessagesReceived.Remove(mensaje);
            MessagesSended.Remove(mensaje);
        }

        public void MarcarMensajeComoLeido(Message mensaje) => mensaje.MarcarComoLeido();

        public void MarcarMensajeComoNoLeido(Message mensaje) => mensaje.MarcarComoNoLeido();

        #endregion
    }
}