using System.Collections.Generic;
using ContactosModel.Model;

namespace EntityFrameworkDB.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EntityFrameworkDB.ContactosContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EntityFrameworkDB.ContactosContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Usuario.AddOrUpdate(u => u.Id,
                new Usuario() { Login = "david", Password = "david", Nombre = "David", Apellidos = "Patatín Patatán", Foto = "no", Contactos = new List<Usuario>(), ContactoDe = new List<Usuario>(), MensajesEnviados = new List<Mensaje>(), MensajesRecibidos = new List<Mensaje>() },
                new Usuario() { Login = "luis", Password = "luis", Nombre = "Luis", Apellidos = "Patatín Patatán", Foto = "no", Contactos = new List<Usuario>(), ContactoDe = new List<Usuario>(), MensajesEnviados = new List<Mensaje>(), MensajesRecibidos = new List<Mensaje>() },
                new Usuario() { Login = "juan", Password = "juan", Nombre = "Juan", Apellidos = "Patatín Patatán", Foto = "no", Contactos = new List<Usuario>(), ContactoDe = new List<Usuario>(), MensajesEnviados = new List<Mensaje>(), MensajesRecibidos = new List<Mensaje>() },
                new Usuario() { Login = "cristina", Password = "cristina", Nombre = "Cristina", Apellidos = "Patatín Patatán", Foto = "no", Contactos = new List<Usuario>(), ContactoDe = new List<Usuario>(), MensajesEnviados = new List<Mensaje>(), MensajesRecibidos = new List<Mensaje>() },
                new Usuario() { Login = "maria", Password = "maria", Nombre = "Maria", Apellidos = "Patatín Patatán", Foto = "no", Contactos = new List<Usuario>(), ContactoDe = new List<Usuario>(), MensajesEnviados = new List<Mensaje>(), MensajesRecibidos = new List<Mensaje>() },
                new Usuario() { Login = "alex", Password = "alex", Nombre = "Alex", Apellidos = "Patatín Patatán", Foto = "no", Contactos = new List<Usuario>(), ContactoDe = new List<Usuario>(), MensajesEnviados = new List<Mensaje>(), MensajesRecibidos = new List<Mensaje>() }
                );
            context.SaveChanges();

            Usuario user1 = context.Usuario.Find(1);
            if (user1 != null)
            {
                user1.Contactos.Add(context.Usuario.Find(2));
                user1.Contactos.Add(context.Usuario.Find(3));
                user1.Contactos.Add(context.Usuario.Find(4));
            }
            context.SaveChanges();

            context.Mensaje.AddOrUpdate(m => m.Id,
                new Mensaje() { Asunto = "Mensaje de Prueba a luis", Contenido = "Este es un Mensaje de Prueba. Se puede borrar.", Fecha = DateTime.Now, Leido = false, Origen = context.Usuario.Find(1), Destino = context.Usuario.Find(2) },
                new Mensaje() { Asunto = "Mensaje de Prueba a juan", Contenido = "Este es un Mensaje de Prueba. Se puede borrar.", Fecha = DateTime.Now, Leido = false, Origen = context.Usuario.Find(1), Destino = context.Usuario.Find(3) },
                new Mensaje() { Asunto = "Mensaje de Prueba a cristina", Contenido = "Este es un Mensaje de Prueba. Se puede borrar.", Fecha = DateTime.Now, Leido = false, Origen = context.Usuario.Find(1), Destino = context.Usuario.Find(4) }
                );
            context.SaveChanges();
        }
    }
}
