using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using DomainModels.Model;
using Repositorio.RepositorioModels;

namespace EntityFrameworkDB.Repositorios
{
    public class ContactoRepositorio : IContactoRepositorio
    {
        private bool _disposed;
        private readonly DbContext _context;
        public DbContext Context => _context;

        public ContactoRepositorio(DbContext context)
        {
            _context = context;
        }

        public virtual void Delete(Contacto model) => _context.Entry(model).State = EntityState.Deleted;

        public virtual void Update(Contacto model) => _context.Entry(model).State = EntityState.Modified;

        public virtual ICollection<Contacto> Get(Expression<Func<Contacto, bool>> expression) => _context.Set<Contacto>().Where(expression).ToList();

        public void Add(Contacto model)
        {
            _context.Set<Contacto>().Add(model);
            _context.Entry(model).State = EntityState.Added;
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