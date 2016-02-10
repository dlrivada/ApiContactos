using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DomainModels.Model;
using RepositorioAdapter.Repositorio;
using System.Data.Entity.Core;
using System.Data.SqlClient;

namespace EntityFrameworkDB.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private bool disposed = false;
        private readonly DbContext _context;
        public DbContext Context => _context;

        public UsuarioRepositorio(DbContext context)
        {
            _context = context;
        }

        public virtual void Delete(Usuario model) => _context.Entry(model).State = EntityState.Deleted;

        public virtual void Update(Usuario model) => _context.Entry(model).State = EntityState.Modified;

        public virtual ICollection<Usuario> Get(Expression<Func<Usuario, bool>> expression) => _context.Set<Usuario>().Where(expression).ToList();

        public Usuario Validar(string login, string password) => _context.Set<Usuario>().Single(o => o.Login == login && o.Password == password);

        public void Add(Usuario model)
        {
            _context.Set<Usuario>().Add(model);
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
            if (disposed)
                return;

            if (disposing)
            {
                Save();
                _context.Dispose();
            }

            disposed = true;
        }

    }
}