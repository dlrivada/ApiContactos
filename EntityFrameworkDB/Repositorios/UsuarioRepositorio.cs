using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DomainModels.Model;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using Repositorio.RepositorioModels;

namespace EntityFrameworkDB.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private bool _disposed;
        private readonly DbContext _context;

        public UsuarioRepositorio(DbContext context)
        {
            _context = context;
        }

        public virtual void Update(Usuario auth, Usuario model) => _context.Entry(model).State = EntityState.Modified;

        public virtual Usuario Get(Usuario auth, Expression<Func<Usuario, bool>> expression)
        {
            // TODO: Comprobar que el usuario está autorizado y autenticado
            // Los campos usuario, origen, model y model.destino no pueden ser nulos
            if (auth == null || expression == null)
                return null; // TODO: Lanzar un error personalizado

            return _context.Set<Contacto>().Where(expression).First();
        }

        public Usuario Validar(string login, string password) => _context.Set<Contacto>().Single(o => o.Login == login && o.Password == password);

        public void Add(Usuario model)
        {
            Contacto contacto = _context.Set<Contacto>().Find(model.Id);
            _context.Set<Contacto>().Add(contacto);
            _context.Entry(contacto).State = EntityState.Added;
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (UpdateException ex)
            {
                var sqlException = ex.InnerException as SqlException;

                if (sqlException != null && sqlException.Errors.OfType<SqlError>()
                    .Any(se => se.Number == 2601 || se.Number == 2627 /* PK/UKC violation */))
                {

                    // TODO: it's a dupe... do something about it
                }
                else {
                    throw;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                Save();
                _context.Dispose();
            }

            _disposed = true;
        }

    }
}