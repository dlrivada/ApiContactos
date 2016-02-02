using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ContactosModel.Model;
using RepositorioAdapter.Repositorio;

namespace EntityFrameworkDB.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        // Contexto de conexión y almacén de instacias del modelo
        // Ojo! Cuidado con replicar (o multiplicar) toda la Base de Datos en la Memoria.
        // Además es necesario impedir al usuario del repositorio que instacie el Contexto para
        // para tener nosotros siempre el control de la memoria consumida y del estado de la conexión
        // para eso hay que usar IoC 
        // Para acceder al almacen de instacias usar el DbSet<> del Context
        private readonly DbContext _context;
        public DbContext Context => _context;


        public virtual int Delete(params object[] keys)
        {
            Usuario data = _context.Set<Usuario>().Find(keys);
            _context.Set<Usuario>().Remove(data);
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public virtual int Delete(Usuario model)
        {
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

        public virtual Usuario Get(params object[] keys) => _context.Set<Usuario>().Find(keys);
        public virtual ICollection<Usuario> Get(Expression<Func<Usuario, bool>> expression) => _context.Set<Usuario>().Where(expression).ToList();
        public virtual ICollection<Usuario> Get() => _context.Set<Usuario>().ToList();

        public UsuarioRepositorio(DbContext context)
        {
            _context = context;
        }
        public Usuario Validar(string login, string password)
        {
            ICollection<Usuario> usuario = Get(o => o.Login == login && o.Password == password);
            return usuario.Any() ? usuario.First() : null;
        }
        public bool IsUnico(string login) => !Get(o => o.Login == login).Any();

        public Usuario Add(Usuario model)
        {
            if (!IsUnico(model.Login)) return null;
            Usuario guardado = model;
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

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}