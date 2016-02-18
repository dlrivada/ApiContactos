﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Domain.Model.ContactAggregate;

namespace Infrastructure.EntityFramework
{
    public class MessageRepositoryEf : IMessageRepository
    {
        private bool _disposed;
        private readonly DbContext _context;

        public void Save()
        {
            _context.SaveChanges();
        }

        // Contexto de conexión y almacén de instacias del modelo
        // Ojo! Cuidado con replicar (o multiplicar) toda la Base de Datos en la Memoria.
        // Además es necesario impedir al usuario del repositorio que instacie el Contexto para
        // para tener nosotros siempre el control de la memoria consumida y del estado de la conexión
        // para eso hay que usar IoC 
        // Para acceder al almacen de instacias usar el DbSet<> del Context
        public MessageRepositoryEf()
        {
            _context = new ContactsUow();
        }

        public virtual void Add(Usuario auth, Message model)
        {
            Contact origen = _context.Set<Contact>().First(c => c.Id == auth.Id);

            // TODO: Comprobar que el usuario está autorizado y autenticado
            // Los campos usuario, origen, model y model.destino no pueden ser nulos
            if (auth == null || origen == null || model?.To == null)
                return; // TODO: Lanzar un error personalizado
            // La entidad no debe existir ya
            if (!_context.Set<Message>().Any(m => m.Id == model.Id))
                return; // TODO: Lanzar un error personalizado
            // El origen y el usuario logueado son el mismo
            if (auth.Login != origen.Login || auth.Password != origen.Password)
                return; // TODO: Lanzar un error personalizado
            // El destino existe
            if (!_context.Set<Contact>().Any(u => u.Login == model.To.Login))
                return; // TODO: Lanzar un error personalizado
            // Al menos tiene contenido
            if (model.Body == string.Empty)
                return; // TODO: Lanzar un error personalizado

            origen.AddMensaje(model.To, model.Issue, model.Body);
            _context.Set<Message>().Add(model);
            _context.Entry(model).State = EntityState.Added;
        }

        public virtual ICollection<Message> Get(Usuario auth, Expression<Func<Message, bool>> expression)
        {
            // TODO: Comprobar que el usuario está autorizado y autenticado
            // Los campos usuario, origen, model y model.destino no pueden ser nulos
            if (auth == null || expression == null)
                return null; // TODO: Lanzar un error personalizado

            return _context.Set<Message>().Where(expression).ToList();
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