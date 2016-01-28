using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ContactosModel.Model;
using RepositorioAdapter.Repositorio;

namespace EntityFrameworkDB.Repositorios
{
    public class MensajeRepositorio : IRepositorioCanRead<Mensaje>, IRepositorioCanAdd<Mensaje>, IRepositorioCanDelete<Mensaje>, IRepositorioCanUpdate<Mensaje>, IRepositorio
    {
        #region UnitOfWork

        // Contexto de conexión y almacén de instacias del modelo
        // Ojo! Cuidado con replicar (o multiplicar) toda la Base de Datos en la Memoria.
        // Además es necesario impedir al usuario del repositorio que instacie el Contexto para
        // para tener nosotros siempre el control de la memoria consumida y del estado de la conexión
        // para eso hay que usar IoC 
        // Para acceder al almacen de instacias usar el DbSet<> del Context
        private readonly DbContext _context;

        public MensajeRepositorio(DbContext context)
        {
            _context = context;
        }

        #endregion

        #region CanAdd

        public virtual Mensaje Add(Mensaje model)
        {
            Mensaje guardado = model;
            _context.Set<Mensaje>().Add(guardado);
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

        #region CanDelete

        public virtual int Delete(params object[] keys) => Delete(_context.Set<Mensaje>().Find(keys));

        public virtual int Delete(Mensaje model)
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

        public virtual int Delete(Expression<Func<Mensaje, bool>> expression)
        {
            IQueryable<Mensaje> guardar = _context.Set<Mensaje>().Where(expression);

            foreach (Mensaje mensaje in guardar)
                ReferenceIntegrity(mensaje);

            _context.Set<Mensaje>().RemoveRange(guardar);

            try
            {
                return _context.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private void ReferenceIntegrity(Mensaje model)
        {
            model.Origen.MensajesEnviados.Remove(model);
            _context.Entry(model.Origen).State = EntityState.Modified;
            model.Destino.MensajesRecibidos.Remove(model);
            _context.Entry(model.Destino).State = EntityState.Modified;
        }

        #endregion

        #region CanUpdate

        public virtual int Update(Mensaje model)
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

        public virtual Mensaje Get(params object[] keys) => _context.Set<Mensaje>().Find(keys);

        public virtual ICollection<Mensaje> Get(Expression<Func<Mensaje, bool>> expression, int pageIndex, int pageSize, out int numberOfPages)
        {
            if (pageSize <= 0)
                pageSize = 5;

            int numberOfelements = _context.Set<Mensaje>().Where(expression).Count();

            if (numberOfelements < pageSize)
                pageSize = numberOfelements;

            numberOfPages = numberOfelements / pageSize;

            if (pageIndex > numberOfPages || pageIndex <= 0)
                pageIndex = numberOfPages;

            return _context.Set<Mensaje>().Where(expression).Skip((pageIndex - 1) * pageSize).Take(pageSize).OrderByDescending(o => o.Fecha).ToList();
        }

        public virtual ICollection<Mensaje> Get(int pageIndex, int pageSize, out int numberOfPages)
        {
            if (pageSize <= 0)
                pageSize = 5;

            int numberOfelements = _context.Set<Mensaje>().Count();

            if (numberOfelements < pageSize)
                pageSize = numberOfelements;

            numberOfPages = numberOfelements / pageSize;

            if (pageIndex > numberOfPages || pageIndex <= 0)
                pageIndex = numberOfPages;

            return _context.Set<Mensaje>().Skip((pageIndex - 1) * pageSize).Take(pageSize).OrderByDescending(o => o.Fecha).ToList();
        }

        #endregion

        #region Mensaje Repository Specific Methods

        public ICollection<Mensaje> GetByDestino(int idDestino, int pageIndex, int pageSize, out int numberOfPages) => Get(o => o.IdDestino == idDestino, pageIndex, pageSize, out numberOfPages).ToList();

        public ICollection<Mensaje> GetByOrigen(int idOrigen, int pageIndex, int pageSize, out int numberOfPages) => Get(o => o.IdOrigen == idOrigen, pageIndex, pageSize, out numberOfPages).ToList();

        #endregion

        #region IDisposable

        public void Dispose()
        {
            _context.Dispose();
        }

        #endregion
    }
}