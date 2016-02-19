using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using Domain.Model.ContactAggregate;

namespace Infrastructure.EntityFramework
{
    public class ContactRepositoryEf : IContactRepository
    {
        private bool _disposed;
        private readonly DbContext _context;

        public ContactRepositoryEf()
        {
            _context = new ContactsContext();
        }

        public virtual void Delete(Contact model) => _context.Entry(model).State = EntityState.Deleted;

        public virtual void Update(Contact model) => _context.Entry(model).State = EntityState.Modified;

        public virtual ICollection<Contact> Get(string login)
        {
            Contact usuario = _context.Set<Contact>().SingleOrDefault(c => c.Login == login);
            if (usuario == null)
                return null;
            _context.Entry(usuario).Collection(c => c.Contacts).Load();
            return usuario.Contacts;
        }

        public void Add(Contact model)
        {
            _context.Set<Contact>().Add(model);
            _context.Entry(model).State = EntityState.Added;
        }

        public ICollection<Message> SentMessages(string login)
        {
            Contact usuario = _context.Set<Contact>().SingleOrDefault(c => c.Login == login);
            if (usuario == null)
                return null;
            _context.Entry(usuario).Collection(c => c.MessagesSended).Load();
            _context.Entry(usuario).Collection(c=>c.Contacts).Load();
            return usuario.MessagesSended;
        }

        public virtual void SendMessaje(string login, Message model)
        {
            Contact origen = _context.Set<Contact>().SingleOrDefault(c => c.Login == login);

            if (origen == null)
                return; // TODO: Lanzar un error personalizado
            // La entidad no debe existir ya
            if (!_context.Set<Message>().Any(m => m.Id == model.Id))
                return; // TODO: Lanzar un error personalizado
            // El destino existe
            if (!_context.Set<Contact>().Any(u => u.Login == model.To.Login))
                return; // TODO: Lanzar un error personalizado

            origen.AddMensaje(model.To, model.Issue, model.Body);
            _context.Set<Message>().Add(model);
            _context.Entry(model).State = EntityState.Added;
        }

        public virtual ICollection<Message> InboxMessages(string login)
        {
            Contact usuario = _context.Set<Contact>().SingleOrDefault(c => c.Login == login);
            if (usuario == null)
                return null;
            _context.Entry(usuario).Collection(c => c.MessagesReceived).Load();
            _context.Entry(usuario).Collection(c => c.Contacts).Load();
            return usuario.MessagesReceived;
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (UpdateException ex)
            {
                SqlException sqlException = ex.InnerException as SqlException;

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