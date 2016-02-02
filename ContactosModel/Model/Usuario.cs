using System.Collections.Generic;

namespace ContactosModel.Model
{
    public class Usuario : DomainModel
    {
        public Usuario()
        {
            if (MensajesEnviados == null)
                MensajesEnviados = new List<Mensaje>();
            if (MensajesRecibidos == null)
                MensajesRecibidos = new List<Mensaje>();
            if (Contactos == null)
                Contactos = new List<Usuario>();
            if (ContactoDe == null)
                ContactoDe = new List<Usuario>();
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Foto { get; set; }

        public ICollection<Mensaje> MensajesEnviados { get; set; }
        public ICollection<Mensaje> MensajesRecibidos { get; set; }
        public ICollection<Usuario> Contactos { get; set; }
        public ICollection<Usuario> ContactoDe { get; set; }
    }
}
