using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Domain.Model.ContactAggregate;

namespace Infrastructure.EntityFramework.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ContactsUow>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ContactsUow context)
        {
            List<Contact> usuarios = new List<Contact>()
            {
                new Contact ("david.lozano.rivada@tajamar365.com", "password", "Usuario 1", "Apellidos 1"),
                new Contact ("user2@dominio.com", "password", "Usuario 2", "Apellidos 2"),
                new Contact ("user3@dominio.com", "password", "Usuario 3", "Apellidos 3"),
                new Contact ("user4@dominio.com", "password", "Usuario 4", "Apellidos 4"),
                new Contact ("user5@dominio.com", "password", "Usuario 5", "Apellidos 5"),
                new Contact ("user6@dominio.com", "password", "Usuario 6", "Apellidos 6")
            };

            context.SaveChanges();

            usuarios.Single(c => c.Login == "david.lozano.rivada@tajamar365.com").Contacts.Add(usuarios.Single(u => u.Login == "user2@dominio.com"));
            usuarios.Single(c => c.Login == "david.lozano.rivada@tajamar365.com").Contacts.Add(usuarios.Single(u => u.Login == "user3@dominio.com"));
            usuarios.Single(c => c.Login == "david.lozano.rivada@tajamar365.com").Contacts.Add(usuarios.Single(u => u.Login == "user5@dominio.com"));

            usuarios.Single(u => u.Login == "david.lozano.rivada@tajamar365.com").AddMensaje(usuarios.Single(u => u.Login == "user2@dominio.com"), "Mensaje prueba 1", "Este es un mensaje de prueba. 1.");
            usuarios.Single(u => u.Login == "david.lozano.rivada@tajamar365.com").AddMensaje(usuarios.Single(u => u.Login == "user2@dominio.com"), "Mensaje prueba 2", "Este es un mensaje de prueba. 2.");
            usuarios.Single(u => u.Login == "david.lozano.rivada@tajamar365.com").AddMensaje(usuarios.Single(u => u.Login == "user3@dominio.com"), "Mensaje prueba 3", "Este es un mensaje de prueba. 3.");

            usuarios.Single(c => c.Login == "user2@dominio.com").ContactsFrom.Add(usuarios.Single(u => u.Login == "david.lozano.rivada@tajamar365.com"));
            usuarios.Single(c => c.Login == "user2@dominio.com").ContactsFrom.Add(usuarios.Single(u => u.Login == "user4@dominio.com"));

            usuarios.Single(c => c.Login == "user3@dominio.com").ContactsFrom.Add(usuarios.Single(u => u.Login == "david.lozano.rivada@tajamar365.com"));

            usuarios.Single(c => c.Login == "user4@dominio.com").Contacts.Add(usuarios.Single(u => u.Login == "user6@dominio.com"));
            usuarios.Single(c => c.Login == "user4@dominio.com").ContactsFrom.Add(usuarios.Single(u => u.Login == "user2@dominio.com"));

            usuarios.Single(u => u.Login == "user4@dominio.com").AddMensaje(usuarios.Single(u => u.Login == "user2@dominio.com"), "Mensaje prueba 4", "Este es un mensaje de prueba. 4.");
            usuarios.Single(u => u.Login == "user4@dominio.com").AddMensaje(usuarios.Single(u => u.Login == "user6@dominio.com"), "Mensaje prueba 5", "Este es un mensaje de prueba. 5.");
            usuarios.Single(u => u.Login == "user4@dominio.com").AddMensaje(usuarios.Single(u => u.Login == "user6@dominio.com"), "Mensaje prueba 6", "Este es un mensaje de prueba. 6.");
            usuarios.Single(u => u.Login == "user4@dominio.com").AddMensaje(usuarios.Single(u => u.Login == "user2@dominio.com"), "Mensaje prueba 7", "Este es un mensaje de prueba. 7.");
            usuarios.Single(u => u.Login == "user4@dominio.com").AddMensaje(usuarios.Single(u => u.Login == "david.lozano.rivada@tajamar365.com"), "Mensaje prueba 4", "Este es un mensaje de prueba. 4.");
            usuarios.Single(u => u.Login == "user4@dominio.com").AddMensaje(usuarios.Single(u => u.Login == "david.lozano.rivada@tajamar365.com"), "Mensaje prueba 5", "Este es un mensaje de prueba. 5.");
            usuarios.Single(u => u.Login == "user4@dominio.com").AddMensaje(usuarios.Single(u => u.Login == "david.lozano.rivada@tajamar365.com"), "Mensaje prueba 6", "Este es un mensaje de prueba. 6.");
            usuarios.Single(u => u.Login == "user4@dominio.com").AddMensaje(usuarios.Single(u => u.Login == "david.lozano.rivada@tajamar365.com"), "Mensaje prueba 7", "Este es un mensaje de prueba. 7.");

            usuarios.Single(c => c.Login == "user5@dominio.com").ContactsFrom.Add(usuarios.Single(u => u.Login == "david.lozano.rivada@tajamar365.com"));

            usuarios.Single(c => c.Login == "user6@dominio.com").ContactsFrom.Add(usuarios.Single(u => u.Login == "user4@dominio.com"));

            foreach (Contact usuario in usuarios)
            {
                context.User.AddOrUpdate(u => u.Login, usuario);
                foreach (Message mensajesEnviado in usuario.MessagesSended)
                    context.Message.AddOrUpdate(m => m.Id, mensajesEnviado);
            }


            context.SaveChanges();
        }
    }
}
