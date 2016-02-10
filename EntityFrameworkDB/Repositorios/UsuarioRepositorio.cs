﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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
        public UsuarioRepositorio(DbContext context)
        {
            _context = context;
        }

        public virtual void Delete(Usuario model) => _context.Entry(model).State = EntityState.Deleted;

        public virtual void Update(Usuario model) => _context.Entry(model).State = EntityState.Modified;

        public virtual ICollection<Usuario> Get(Expression<Func<Usuario, bool>> expression) => _context.Set<Usuario>().Where(expression).ToList();

        public Usuario Validar(string login, string password) => _context.Set<Usuario>().Single(o => o.Login == login && o.Password == password);

        public void Add(Usuario model) => _context.Set<Usuario>().Add(model);

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}