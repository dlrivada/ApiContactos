using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using ContactosModel.Model;

namespace EntityFrameworkDB
{
    using System.Data.Entity;

    public class Model1 : DbContext
    {
        // Your context has been configured to use a 'Model1' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'EntityFrameworkDB.Model1' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model1' 
        // connection string in the application configuration file.
        public Model1() : base("name=Model1")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Mensaje> Mensaje { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Configuration.LazyLoadingEnabled = false;
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Usuario>().HasKey(uw => uw.Id);
            modelBuilder.Entity<Usuario>().Property(uw => uw.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Usuario>().Property(uw => uw.Login).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Usuario>().Property(uw => uw.Password).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Usuario>().Property(uw => uw.Nombre).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Usuario>().Property(uw => uw.Apellidos).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Usuario>().Property(uw => uw.Foto).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Mensaje>().ToTable("Mensaje");
            modelBuilder.Entity<Mensaje>().HasKey(ur => ur.Id);
            modelBuilder.Entity<Mensaje>().Property(ur => ur.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Mensaje>().Property(ur => ur.Asunto).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Mensaje>().Property(ur => ur.Contenido).IsRequired().HasMaxLength(500);

            modelBuilder.Entity<Usuario>()
            .HasMany(entity => entity.Contactos)
            .WithMany(child => child.ContactoDe)
            .Map(map =>
            {
                map.ToTable("Contacto");
                map.MapLeftKey("IdUsuario");
                map.MapRightKey("IdAmigo");
            });

            modelBuilder.Entity<Usuario>()
                .HasMany(t => t.MensajesEnviados)
                .WithRequired(t => t.Origen);

            modelBuilder.Entity<Usuario>()
                .HasMany(t => t.MensajesRecibidos)
                .WithRequired(t => t.Destino);
        }
    }
}