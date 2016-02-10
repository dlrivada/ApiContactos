using System;
using DomainModels.Base;

namespace DomainModels.Model
{
    public class Mensaje : Identity
    {
        public Mensaje(Contacto origen, Contacto destino, string contenido) : this(origen, destino, string.Empty, contenido)
        {
        }

        public Mensaje(Contacto origen, Contacto destino, string asunto, string contenido)
        {
            Origen = origen;
            Destino = destino;
            Asunto = asunto;
            Contenido = contenido;
            Leido = false;
            Fecha = DateTime.Now;
        }

        protected Mensaje()
        {

        }

        public string Asunto { get; private set; }
        public string Contenido { get; private set; }
        public bool Leido { get; private set; }
        public System.DateTime Fecha { get; private set; }

        public Contacto Destino { get; private set; }
        public Contacto Origen { get; private set; }

        public void MarcarComoLeido()
        {
            Leido = true;
        }
        public void MarcarComoNoLeido()
        {
            Leido = false;
        }

    }
}