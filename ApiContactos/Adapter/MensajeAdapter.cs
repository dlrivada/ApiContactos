using ApiContactos.Models;
using ContactosModel.Model;
using RepositorioAdapter.Adapter;

namespace ApiContactos.Adapter
{
    public class MensajeAdapter : BaseAdapter<Mensaje, MensajeModel>
    {
        public override Mensaje FromViewModel(MensajeModel model) => new Mensaje()
        {
            id = model.id,
            idOrigen = model.idOrigen,
            idDestino = model.idDestino,
            Asunto = model.Asunto,
            Contenido = model.Contenido,
            Leido = model.Leido,
            Fecha = model.Fecha
        };

        public override MensajeModel FromModel(Mensaje model) => new MensajeModel()
        {
            id = model.id,
            idOrigen = model.idOrigen,
            idDestino = model.idDestino,
            Asunto = model.Asunto,
            Contenido = model.Contenido,
            Leido = model.Leido,
            Fecha = model.Fecha
        };
    }
}