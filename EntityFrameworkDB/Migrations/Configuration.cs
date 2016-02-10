using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using DomainModels.Model;

namespace EntityFrameworkDB.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Model1>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Model1 context)
        {
            List<Contacto> usuarios = new List<Contacto>()
            {
                new Contacto ("user1", "user1", "Usuario 1", "Apellidos 1"),
                new Contacto ("user2", "user2", "Usuario 2", "Apellidos 2"),
                new Contacto ("user3", "user3", "Usuario 3", "Apellidos 3"),
                new Contacto ("user4", "user4", "Usuario 4", "Apellidos 4"),
                new Contacto ("user5", "user5", "Usuario 5", "Apellidos 5"),
                new Contacto ("user6", "user6", "Usuario 6", "Apellidos 6")
            };

            usuarios.Single(c => c.Login == "user1")
                .Contactos.Add(usuarios.Single(u => u.Login == "user2"));
            usuarios.Single(c => c.Login == "user1")
                .Contactos.Add(usuarios.Single(u => u.Login == "user3"));
            usuarios.Single(c => c.Login == "user1")
                .Contactos.Add(usuarios.Single(u => u.Login == "user5"));

            usuarios.Single(u => u.Login == "user1").AddMensaje(usuarios.Single(u => u.Login == "user2"), "Mensaje prueba 1", "Este es un mensaje de prueba. 1.");
            usuarios.Single(u => u.Login == "user1").AddMensaje(usuarios.Single(u => u.Login == "user2"), "Mensaje prueba 2", "Este es un mensaje de prueba. 2.");
            usuarios.Single(u => u.Login == "user1").AddMensaje(usuarios.Single(u => u.Login == "user3"), "Mensaje prueba 3", "Este es un mensaje de prueba. 3.");

            usuarios.Single(c => c.Login == "user2")
                .ContactoDe.Add(usuarios.Single(u => u.Login == "user1"));
            usuarios.Single(c => c.Login == "user2")
                .ContactoDe.Add(usuarios.Single(u => u.Login == "user4"));

            usuarios.Single(c => c.Login == "user3")
                .ContactoDe.Add(usuarios.Single(u => u.Login == "user1"));

            usuarios.Single(c => c.Login == "user4")
                .Contactos.Add(usuarios.Single(u => u.Login == "user6"));
            usuarios.Single(c => c.Login == "user4")
                .ContactoDe.Add(usuarios.Single(u => u.Login == "user2"));

            usuarios.Single(u => u.Login == "user4").AddMensaje(usuarios.Single(u => u.Login == "user2"), "Mensaje prueba 4", "Este es un mensaje de prueba. 4.");
            usuarios.Single(u => u.Login == "user4").AddMensaje(usuarios.Single(u => u.Login == "user6"), "Mensaje prueba 5", "Este es un mensaje de prueba. 5.");
            usuarios.Single(u => u.Login == "user4").AddMensaje(usuarios.Single(u => u.Login == "user6"), "Mensaje prueba 6", "Este es un mensaje de prueba. 6.");
            usuarios.Single(u => u.Login == "user4").AddMensaje(usuarios.Single(u => u.Login == "user2"), "Mensaje prueba 7", "Este es un mensaje de prueba. 7.");

            usuarios.Single(c => c.Login == "user5")
                .ContactoDe.Add(usuarios.Single(u => u.Login == "user1"));

            usuarios.Single(c => c.Login == "user6")
                .ContactoDe.Add(usuarios.Single(u => u.Login == "user4"));

            foreach (Contacto usuario in usuarios)
            {
                context.Usuario.AddOrUpdate(u => u.Login, usuario);
                foreach (Mensaje mensajesEnviado in usuario.MensajesEnviados)
                    context.Mensaje.AddOrUpdate(m => m.Id, mensajesEnviado);
            }


            context.SaveChanges();
        }
    }
}
