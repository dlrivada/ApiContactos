using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

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
            List<Usuario> usuarios = new List<Usuario>()
            {
                new Usuario { Nombre = "Usuario 1", Apellidos = "Apellidos 1", Login = "user1", Password = "user1", Foto = "NoFoto" },
                new Usuario { Nombre = "Usuario 2", Apellidos = "Apellidos 2", Login = "user2", Password = "user2", Foto = "NoFoto" },
                new Usuario { Nombre = "Usuario 3", Apellidos = "Apellidos 3", Login = "user3", Password = "user3", Foto = "NoFoto" },
                new Usuario { Nombre = "Usuario 4", Apellidos = "Apellidos 4", Login = "user4", Password = "user4", Foto = "NoFoto" },
                new Usuario { Nombre = "Usuario 5", Apellidos = "Apellidos 5", Login = "user5", Password = "user5", Foto = "NoFoto" },
                new Usuario { Nombre = "Usuario 6", Apellidos = "Apellidos 6", Login = "user6", Password = "user6", Foto = "NoFoto" }
            };

            usuarios.Single(c => c.Login == "user1")
                .Contactos.Add(usuarios.Single(u => u.Login == "user2"));
            usuarios.Single(c => c.Login == "user1")
                .Contactos.Add(usuarios.Single(u => u.Login == "user3"));
            usuarios.Single(c => c.Login == "user1")
                .Contactos.Add(usuarios.Single(u => u.Login == "user5"));

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

            usuarios.Single(c => c.Login == "user5")
                .ContactoDe.Add(usuarios.Single(u => u.Login == "user1"));

            usuarios.Single(c => c.Login == "user6")
                .ContactoDe.Add(usuarios.Single(u => u.Login == "user4"));

            foreach (Usuario usuario in usuarios)
                context.Usuario.AddOrUpdate(u => u.Login, usuario);
            context.SaveChanges();

            context.Mensaje.AddOrUpdate(m => m.Id,
                new Mensaje { Asunto = "Mensaje prueba 1", Contenido = "Este es un mensaje de prueba. 1.", Leido = false, Fecha = DateTime.Now, Origen = usuarios.Single(u => u.Login == "user1"), Destino = usuarios.Single(u => u.Login == "user2") },
                new Mensaje { Asunto = "Mensaje prueba 2", Contenido = "Este es un mensaje de prueba. 2.", Leido = false, Fecha = DateTime.Now, Origen = usuarios.Single(u => u.Login == "user1"), Destino = usuarios.Single(u => u.Login == "user2") },
                new Mensaje { Asunto = "Mensaje prueba 3", Contenido = "Este es un mensaje de prueba. 3.", Leido = false, Fecha = DateTime.Now, Origen = usuarios.Single(u => u.Login == "user1"), Destino = usuarios.Single(u => u.Login == "user3") },
                new Mensaje { Asunto = "Mensaje prueba 4", Contenido = "Este es un mensaje de prueba. 4.", Leido = false, Fecha = DateTime.Now, Origen = usuarios.Single(u => u.Login == "user4"), Destino = usuarios.Single(u => u.Login == "user2") },
                new Mensaje { Asunto = "Mensaje prueba 5", Contenido = "Este es un mensaje de prueba. 5.", Leido = false, Fecha = DateTime.Now, Origen = usuarios.Single(u => u.Login == "user4"), Destino = usuarios.Single(u => u.Login == "user6") },
                new Mensaje { Asunto = "Mensaje prueba 6", Contenido = "Este es un mensaje de prueba. 6.", Leido = false, Fecha = DateTime.Now, Origen = usuarios.Single(u => u.Login == "user4"), Destino = usuarios.Single(u => u.Login == "user6") },
                new Mensaje { Asunto = "Mensaje prueba 7", Contenido = "Este es un mensaje de prueba. 7.", Leido = false, Fecha = DateTime.Now, Origen = usuarios.Single(u => u.Login == "user4"), Destino = usuarios.Single(u => u.Login == "user2") }
                );
            context.SaveChanges();
        }
    }
}
