namespace ContactosModel.Model
{
    public class MensajeModel
    {
        public int id { get; set; }
        public int idOrigen { get; set; }
        public int idDestino { get; set; }
        public string Asunto { get; set; }
        public string Contenido { get; set; }
        public bool Leido { get; set; }
        public System.DateTime Fecha { get; set; }
    }
}