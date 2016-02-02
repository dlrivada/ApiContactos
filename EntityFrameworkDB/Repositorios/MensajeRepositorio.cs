using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ContactosModel.Model;
using RepositorioAdapter.Repositorio;

namespace EntityFrameworkDB.Repositorios
{
    public class MensajeRepositorio : IMensajeRepositorio
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

        public virtual void Add(Mensaje model) => _context.Set<Mensaje>().Add(model);

        public virtual void Delete(Mensaje model) => _context.Entry(model).State = EntityState.Deleted;

        public virtual void Update(Mensaje model) => _context.Entry(model).State = EntityState.Modified;

        public virtual ICollection<Mensaje> Get(Expression<Func<Mensaje, bool>> expression) => _context.Set<Mensaje>().Where(expression).ToList();

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}