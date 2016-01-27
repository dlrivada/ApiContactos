using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ContactosModel.Model;
using RepositorioAdapter.Repositorio;

namespace EntityFrameworkDB.Repositorios
{
    public class MensajeRepositorio : IRepositorioCanRead<Mensaje>, IRepositorioCanAdd<Mensaje>, IRepositorioCanDelete<Mensaje>, IRepositorioCanUpdate<Mensaje>, IRepositorio<Usuario>, IDisposable
    {
        private readonly DbContext _context;
        public DbContext Context => _context;

        // Contexto de conexión y almacén de instacias del modelo
        // Ojo! Cuidado con replicar (o multiplicar) toda la Base de Datos en la Memoria.
        // Además es necesario impedir al usuario del repositorio que instacie el Contexto para
        // para tener nosotros siempre el control de la memoria consumida y del estado de la conexión
        // para eso hay que usar IoC 
        // Para acceder al almacen de instacias usar el DbSet<> del Context

        public MensajeRepositorio(DbContext context)
        {
            _context = context;
        }

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

        public virtual int Delete(params object[] keys)
        {
            Mensaje data = _context.Set<Mensaje>().Find(keys);
            _context.Set<Mensaje>().Remove(data);
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public virtual int Delete(Mensaje model)
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

        public virtual int Delete(Expression<Func<Mensaje, bool>> expression)
        {
            IQueryable<Mensaje> guardar = _context.Set<Mensaje>().Where(expression);
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

        public virtual Mensaje Get(params object[] keys) => _context.Set<Mensaje>().Find(keys);
        public virtual ICollection<Mensaje> Get(Expression<Func<Mensaje, bool>> expression) => _context.Set<Mensaje>().Where(expression).ToList();
        public virtual ICollection<Mensaje> Get() => _context.Set<Mensaje>().ToList();

        public ICollection<Mensaje> GetByDestino(int idDestino) => Get(o => o.IdDestino == idDestino).OrderByDescending(o => o.Fecha).ToList();

        public ICollection<Mensaje> GetByOrigen(int idOrigen) => Get(o => o.IdOrigen == idOrigen).OrderByDescending(o => o.Fecha).ToList();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}