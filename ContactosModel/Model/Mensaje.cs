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

        public Usuario Destino { get; set; }
        public Usuario Origen { get; set; }
    }
}