using System.Collections.Generic;

namespace DomainModels.Model
{
    public class Contacto : Usuario
    {
        public string Nombre { get; private set; }
        public string Apellidos { get; private set; }
        public string Foto { get; private set; }

        public ICollection<Contacto> Contactos { get; private set; }
        public ICollection<Contacto> ContactoDe { get; private set; }
        public ICollection<Mensaje> MensajesEnviados { get; private set; }
        public ICollection<Mensaje> MensajesRecibidos { get; private set; }


        public Contacto(string login, string password) : this(login, password, string.Empty)
        {
        }

        public Contacto(string login, string password, string nombre) : this(login, password, nombre, string.Empty)
        {
        }

        public Contacto(string login, string password, string nombre, string apellidos) : this(login, password, nombre, apellidos, string.Empty)
        {
        }

        public Contacto(string login, string password, string nombre, string apellidos, string foto) : base(login, password) 
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Foto = foto;

            if (MensajesEnviados == null)
                MensajesEnviados = new List<Mensaje>();

            if (MensajesRecibidos == null)
                MensajesRecibidos = new List<Mensaje>();

            if (Contactos == null)
                Contactos = new List<Contacto>();

            if (ContactoDe == null)
                ContactoDe = new List<Contacto>();
        }

        protected Contacto() : base()
        {
            if (MensajesEnviados == null)
                MensajesEnviados = new List<Mensaje>();

            if (MensajesRecibidos == null)
                MensajesRecibidos = new List<Mensaje>();

            if (Contactos == null)
                Contactos = new List<Contacto>();

            if (ContactoDe == null)
                ContactoDe = new List<Contacto>();
        }


        public void SetContactoDetails(string nombre, string apellidos, string foto)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Foto = foto;
        }

        public void AddContacto(Contacto contacto)
        {
            Contactos.Add(contacto);
            contacto.ContactoDe.Add(this);
        }
        public void RemoveContacto(Contacto contacto)
        {
            Contactos.Remove(contacto);
            contacto.ContactoDe.Remove(this);
        }

        public void AddMensaje(Contacto destino, string asunto, string contenido)
        {
            Mensaje nuevoMensaje = new Mensaje(this, destino, asunto, contenido);
            MensajesEnviados.Add(nuevoMensaje);
            destino.MensajesRecibidos.Add(nuevoMensaje);
        }

        public void AddMensaje(Contacto destino, string contenido) => AddMensaje(destino, string.Empty, contenido);

        public void RemoveMensaje(Mensaje mensaje)
        {
            mensaje.Destino.MensajesRecibidos.Remove(mensaje);
            MensajesEnviados.Remove(mensaje);
        }

        public void MarcarMensajeComoLeido(Mensaje mensaje) => mensaje.MarcarComoLeido();
        public void MarcarMensajeComoNoLeido(Mensaje mensaje) => mensaje.MarcarComoNoLeido();
    }
}