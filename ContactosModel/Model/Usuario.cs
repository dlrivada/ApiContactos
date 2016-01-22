using System.Collections.Generic;

namespace ContactosModel.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Foto { get; set; }

        public virtual ICollection<Mensaje> MensajesEnviados { get; set; }
        public virtual ICollection<Mensaje> MensajesRecibidos { get; set; }
        public virtual ICollection<Mensaje> Usuarios1 { get; set; }
        public virtual ICollection<Mensaje> Amigos { get; set; }
    }
}
