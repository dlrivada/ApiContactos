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
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        #region UnitOfWork

        // Contexto de conexión y almacén de instacias del modelo
        // Ojo! Cuidado con replicar (o multiplicar) toda la Base de Datos en la Memoria.
        // Además es necesario impedir al usuario del repositorio que instacie el Contexto para
        // para tener nosotros siempre el control de la memoria consumida y del estado de la conexión
        // para eso hay que usar IoC. Para acceder al almacen de instacias usar el DbSet<> del Context
        private readonly IObjectContextAdapter _context;
        IObjectSet<Usuario> _objectSet;

        public UsuarioRepositorio(IObjectContextAdapter context)
        {
            _context = context;
            _objectSet = _context.ObjectContext.CreateObjectSet<Usuario>();
        }

        #endregion

        #region CanDelete

        public virtual void Delete(Usuario model) => _objectSet.DeleteObject(model);

        #endregion

        #region CanUpdate

        public void Attach(Usuario entity)
        {
            Attach(entity, EntityStatus.Unchanged);
        }

        public void Attach(Usuario entity, EntityStatus status)
        {
            _objectSet.Attach(entity);
            _context.ObjectContext.ObjectStateManager.ChangeObjectState(entity, GetEntityState(status));
        }

        #endregion

        #region CanRead

        public virtual ICollection<Usuario> Get(Expression<Func<Usuario, bool>> expression, int pageIndex, int pageSize, out int numberOfPages)
        {
            if (pageSize <= 0)
                pageSize = 5;

            int numberOfelements = _objectSet.Where(expression).Count();

            if (numberOfelements < pageSize)
                pageSize = numberOfelements;

            numberOfPages = numberOfelements / pageSize;

            if (pageIndex > numberOfPages || pageIndex <= 0)
                pageIndex = numberOfPages;

            return _objectSet.Where(expression).Skip((pageIndex - 1) * pageSize).Take(pageSize).OrderBy(o => o.Nombre).ThenBy(o => o.Apellidos).ToList();
        }

        public virtual ICollection<Usuario> Get(int pageIndex, int pageSize, out int numberOfPages)
        {
            if (pageSize <= 0)
                pageSize = 5;

            int numberOfelements = _objectSet.Count();

            if (numberOfelements < pageSize)
                pageSize = numberOfelements;

            numberOfPages = numberOfelements / pageSize;

            if (pageIndex > numberOfPages || pageIndex <= 0)
                pageIndex = numberOfPages;

            return _objectSet.Skip((pageIndex - 1) * pageSize).Take(pageSize).OrderBy(o => o.Nombre).ThenBy(o => o.Apellidos).ToList();
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

        public virtual ICollection<Usuario> GetFullUser() => _objectSet.Include(o => o.Contactos).Include(o => o.ContactoDe).Include(o => o.MensajesEnviados).Include(o => o.MensajesRecibidos).ToList();

        #endregion

        #region CanAdd

        public void Add(Usuario model)
        {
            if (IsUnico(model.Login))
                _objectSet.AddObject(model);
        }

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