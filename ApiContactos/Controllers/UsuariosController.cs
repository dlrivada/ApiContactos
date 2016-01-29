using System;
using System.Web.Http;
using System.Web.Http.Description;
using ContactosModel.Model;
using Microsoft.Practices.Unity;
using RepositorioAdapter.Repositorio;
using RepositorioAdapter.UnitOfWork;

namespace ApiContactos.Controllers
{
    public class UsuariosController : ApiController
    {
        IUsuarioRepositorio _usuarioRepositorio;
        IUnitOfWork _unitOfWork;

        public UsuariosController(IUsuarioRepositorio usuarioRepositorio, IUnitOfWork unitOfWork)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult GetValido(string login, string password)
        {
            Usuario data = _usuarioRepositorio.Validar(login, password);
            if (data == null)
                return NotFound();
            return Ok(data);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        public IHttpActionResult GetUnico(string login) => Ok(_usuarioRepositorio.IsUnico(login));

        [HttpPost]
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult Post(Usuario model)
        {
            _usuarioRepositorio.Attach(model, EntityStatus.Modified);
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.SaveChanges();
                }
                catch (ConcurrencyException)
                {
                    ModelState.AddModelError(string.Empty, "The record you attempted to edit was modified by another user after you got the original value. Your edit operation was canceled. If you still want to edit this record, save it again.");
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Unable to save changes. Please try again.");
                }
            }
            return Ok(model);
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, Usuario model)
        {
            Usuario d = _usuarioRepositorio.Get(id);
            if (d == null || d.Id != model.Id)
                return NotFound();

            int data = _usuarioRepositorio.Update(model);
            if (data < 1)
                return BadRequest();
            return Ok();
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id)
        {
            int data = _usuarioRepositorio.Delete(id);
            if (data < 1)
                return BadRequest();
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            _usuarioRepositorio.Dispose();
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
