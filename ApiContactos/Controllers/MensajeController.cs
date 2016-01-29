using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using ContactosModel.Model;
using Microsoft.Practices.Unity;
using RepositorioAdapter.Repositorio;
using RepositorioAdapter.UnitOfWork;

namespace ApiContactos.Controllers
{
    public class MensajeController : ApiController
    {
        IMensajeRepositorio _mensajeRepositorio;
        IUnitOfWork _unitOfWork;

        public MensajeController(IMensajeRepositorio mensajeRepositorio, IUnitOfWork unitOfWork)
        {
            _mensajeRepositorio = mensajeRepositorio;
            _unitOfWork = unitOfWork;
        }

        public ICollection<Mensaje> Get(int id, int pageIndex, int pageSize, out int numberOfPages) => _mensajeRepositorio.GetByDestino(id, pageIndex, pageSize, out numberOfPages);

        public ICollection<Mensaje> Get(int id, bool enviado, int pageIndex, int pageSize, out int numberOfPages) => _mensajeRepositorio.GetByOrigen(id, pageIndex, pageSize, out numberOfPages);

        [ResponseType(typeof(Mensaje))]
        public IHttpActionResult Post(Mensaje model)
        {
            Mensaje data = _mensajeRepositorio.Add(model);
            if (data == null)
                return BadRequest();
            return Ok(data);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult Put(Mensaje model)
        {
            int data = _mensajeRepositorio.Update(model);
            if (data < 1)
                return BadRequest();
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            _mensajeRepositorio.Dispose();
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
