using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using ContactosModel.Model;
using RepositorioAdapter.Repositorio;
using RepositorioAdapter.UnitOfWork;

namespace EntityFrameworkDB.Repositorios
{
    public class MensajeRepositorio : IMensajeRepositorio
    {
        #region UnitOfWork

        // Contexto de conexión y almacén de instacias del modelo
        // Ojo! Cuidado con replicar (o multiplicar) toda la Base de Datos en la Memoria.
        // Además es necesario impedir al usuario del repositorio que instacie el Contexto para
        // para tener nosotros siempre el control de la memoria consumida y del estado de la conexión
        // para eso hay que usar IoC 
        // Para acceder al almacen de instacias usar el DbSet<> del Context
        private readonly IObjectContextAdapter _context;
        IObjectSet<Mensaje> _objectSet;

        public MensajeRepositorio(IObjectContextAdapter context)
        {
            _context = context;
            _objectSet = context.ObjectContext.CreateObjectSet<Mensaje>();
        }

        #endregion

        #region CanAdd

        public virtual void Add(Mensaje model) => _objectSet.AddObject(model);

        #endregion

        #region CanDelete

        public virtual void Delete(Mensaje model) => _objectSet.DeleteObject(model);

        #endregion

        #region CanUpdate

        public void Attach(Mensaje entity)
        {
            Attach(entity, EntityStatus.Unchanged);
        }

        public void Attach(Mensaje entity, EntityStatus status)
        {
            _objectSet.Attach(entity);
            _context.ObjectContext.ObjectStateManager.ChangeObjectState(entity, GetEntityState(status));
        }

        #endregion

        #region CanRead

        public virtual ICollection<Mensaje> Get(Expression<Func<Mensaje, bool>> expression, int pageIndex, int pageSize, out int numberOfPages)
        {
            if (pageSize <= 0)
                pageSize = 5;

            int numberOfelements = _objectSet.Where(expression).Count();

            if (numberOfelements < pageSize)
                pageSize = numberOfelements;

            numberOfPages = numberOfelements / pageSize;

            if (pageIndex > numberOfPages || pageIndex <= 0)
                pageIndex = numberOfPages;

            return _objectSet.Where(expression).Skip((pageIndex - 1) * pageSize).Take(pageSize).OrderByDescending(o => o.Fecha).ToList();
        }

        public virtual ICollection<Mensaje> Get(int pageIndex, int pageSize, out int numberOfPages)
        {
            if (pageSize <= 0)
                pageSize = 5;

            int numberOfelements = _objectSet.Count();

            if (numberOfelements < pageSize)
                pageSize = numberOfelements;

            numberOfPages = numberOfelements / pageSize;

            if (pageIndex > numberOfPages || pageIndex <= 0)
                pageIndex = numberOfPages;

            return _objectSet.Skip((pageIndex - 1) * pageSize).Take(pageSize).OrderByDescending(o => o.Fecha).ToList();
        }

        #endregion

        #region Mensaje Repository Specific Methods

        public ICollection<Mensaje> GetByDestino(int idDestino, int pageIndex, int pageSize, out int numberOfPages) => Get(o => o.Destino.Id == idDestino, pageIndex, pageSize, out numberOfPages).ToList();

        public ICollection<Mensaje> GetByOrigen(int idOrigen, int pageIndex, int pageSize, out int numberOfPages) => Get(o => o.Origen.Id == idOrigen, pageIndex, pageSize, out numberOfPages).ToList();

        #endregion

        #region IDisposable

        public void Dispose()
        {
            _context.ObjectContext.SaveChanges();
            _context?.ObjectContext.Dispose();
            GC.SuppressFinalize(this);
        }

        private EntityState GetEntityState(EntityStatus status)
        {
            switch (status)
            {
                case EntityStatus.Added:
                    return EntityState.Added;
                case EntityStatus.Deleted:
                    return EntityState.Deleted;
                case EntityStatus.Detached:
                    return EntityState.Detached;
                case EntityStatus.Modified:
                    return EntityState.Modified;
                default:
                    return EntityState.Unchanged;
            }
        }

        #endregion
    }
}