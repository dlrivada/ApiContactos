using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Domain.Model.ContactAggregate;

namespace Infrastructure.EntityFramework
{
    public class ContactRepositoryEf : IContactRepository
    {
        private bool _disposed;
        private readonly DbContext _context;

        public ContactRepositoryEf(DbContext context)
        {
            _context = context;
        }

        public virtual void Delete(User auth, Contact model) => _context.Entry(model).State = EntityState.Deleted;

        public virtual void Update(User auth, Contact model) => _context.Entry(model).State = EntityState.Modified;

        public virtual ICollection<Contact> Get(User auth, Expression<Func<Contact, bool>> expression)
        {
            // TODO: Comprobar que el usuario está autorizado y autenticado
            // Los campos usuario, origen, model y model.destino no pueden ser nulos
            if (auth == null || expression == null)
                return null; // TODO: Lanzar un error personalizado

            return _context.Set<Contact>().Where(expression).ToList();
        }

        public void Add(User auth, Contact model)
        {
            _context.Set<Contact>().Add(model);
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