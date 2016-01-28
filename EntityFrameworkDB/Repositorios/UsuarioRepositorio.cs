using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ContactosModel.Model;
using RepositorioAdapter.Repositorio;

namespace EntityFrameworkDB.Repositorios
{
    public class UsuarioRepositorio : IRepositorioCanRead<Usuario>, IRepositorioCanAdd<Usuario>, IRepositorioCanDelete<Usuario>, IRepositorioCanUpdate<Usuario>, IRepositorio
    {
        #region UnitOfWork

        // Contexto de conexión y almacén de instacias del modelo
        // Ojo! Cuidado con replicar (o multiplicar) toda la Base de Datos en la Memoria.
        // Además es necesario impedir al usuario del repositorio que instacie el Contexto para
        // para tener nosotros siempre el control de la memoria consumida y del estado de la conexión
        // para eso hay que usar IoC 
        // Para acceder al almacen de instacias usar el DbSet<> del Context
        private readonly DbContext _context;

        public UsuarioRepositorio(DbContext context)
        {
            _context = context;
        }

        #endregion

        #region CanDelete

        public virtual int Delete(params object[] keys) => Delete(_context.Set<Usuario>().Find(keys));

        public virtual int Delete(Usuario model)
        {
            ReferenceIntegrity(model);

            _context.Entry(model).State = EntityState.Deleted;

            try
            {
                return _context.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public virtual int Delete(Expression<Func<Usuario, bool>> expression)
        {
            IQueryable<Usuario> guardar = _context.Set<Usuario>().Where(expression);

            foreach (Usuario usuario in guardar)
                ReferenceIntegrity(usuario);

            _context.Set<Usuario>().RemoveRange(guardar);

            try
            {
                return _context.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private void ReferenceIntegrity(Usuario model)
        {
            foreach (Usuario contacto in model.Contactos)
            {
                contacto.ContactoDe.Remove(model);
                _context.Entry(contacto).State = EntityState.Modified;
            }

            foreach (Mensaje mensaje in model.MensajesEnviados)
                _context.Entry(mensaje).State = EntityState.Deleted;

            foreach (Mensaje mensaje in model.MensajesRecibidos)
                _context.Entry(mensaje).State = EntityState.Deleted;
        }

        #endregion

        #region CanUpdate

        public virtual int Update(Usuario model)
        {
            _context.Entry(model).State = EntityState.Modified;
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        #endregion

        #region CanRead

        public virtual Usuario Get(params object[] keys) => _context.Set<Usuario>().Find(keys);

        public virtual ICollection<Usuario> Get(Expression<Func<Usuario, bool>> expression, int pageIndex, int pageSize, out int numberOfPages)
        {
            if (pageSize <= 0)
                pageSize = 5;

            int numberOfelements = _context.Set<Usuario>().Where(expression).Count();

            if (numberOfelements < pageSize)
                pageSize = numberOfelements;

            numberOfPages = numberOfelements / pageSize;

            if (pageIndex > numberOfPages || pageIndex <= 0)
                pageIndex = numberOfPages;

            return _context.Set<Usuario>().Where(expression).Skip((pageIndex - 1) * pageSize).Take(pageSize).OrderBy(o => o.Nombre).ThenBy(o => o.Apellidos).ToList();
        }

        public virtual ICollection<Usuario> Get(int pageIndex, int pageSize, out int numberOfPages)
        {
            if (pageSize <= 0)
                pageSize = 5;

            int numberOfelements = _context.Set<Usuario>().Count();

            if (numberOfelements < pageSize)
                pageSize = numberOfelements;

            numberOfPages = numberOfelements / pageSize;

            if (pageIndex > numberOfPages || pageIndex <= 0)
                pageIndex = numberOfPages;

            return _context.Set<Usuario>().Skip((pageIndex - 1) * pageSize).Take(pageSize).OrderBy(o => o.Nombre).ThenBy(o => o.Apellidos).ToList();
        }

        #endregion

        #region Usuario Repository Specific Methods

        public Usuario Validar(string login, string password)
        {
            int numberOfPages;
            ICollection<Usuario> usuario = Get(o => o.Login == login && o.Password == password, 1, 1, out numberOfPages);
            return usuario.Any() ? usuario.First() : null;
        }

        public bool IsUnico(string login)
        {
            int numberOfPages;
            return !Get(o => o.Login == login, 1, 1, out numberOfPages).Any();
        }

        public virtual ICollection<Usuario> GetFullUser() => _context.Set<Usuario>().Include(o => o.Contactos).Include(o => o.ContactoDe).Include(o => o.MensajesEnviados).Include(o => o.MensajesRecibidos).ToList();

        #endregion

        #region CanAdd

        public Usuario Add(Usuario model)
        {
            if (!IsUnico(model.Login)) return null;
            Usuario guardado = model;

            foreach (Usuario contacto in model.Contactos)
                guardado.Contactos.Add(contacto);
            foreach (Usuario contacto in model.ContactoDe)
                guardado.ContactoDe.Add(contacto);
            foreach (Mensaje mensaje in model.MensajesEnviados)
                guardado.MensajesEnviados.Add(mensaje);
            foreach (Mensaje mensaje in model.MensajesRecibidos)
                guardado.MensajesRecibidos.Add(mensaje);

            _context.Set<Usuario>().Add(guardado);
            try
            {
                _context.SaveChanges();
                return guardado;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            _context.Dispose();
        }

        #endregion
    }
}