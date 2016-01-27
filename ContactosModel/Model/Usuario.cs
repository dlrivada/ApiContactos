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
        public ICollection<Usuario> Contactos { get; set; }
        public ICollection<Usuario> ContatoDe { get; set; } 
    }
}
