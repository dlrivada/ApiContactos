﻿using System.Collections.Generic;

namespace ContactosModel.Model
{
    public class Mensaje : DomainModel
    {
        public int IdOrigen { get; set; }
        public int IdDestino { get; set; }
        public string Asunto { get; set; }
        public string Contenido { get; set; }
        public bool Leido { get; set; }
        public System.DateTime Fecha { get; set; }

        public virtual Usuario Destino { get; set; }
        public virtual Usuario Origen { get; set; }
    }
}