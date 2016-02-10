using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using DomainModels.Model;

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

        public virtual DbSet<Contacto> Usuario { get; set; }
        public virtual DbSet<Mensaje> Mensaje { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Configuration.LazyLoadingEnabled = false;
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Contacto>().ToTable("Usuario");
            modelBuilder.Entity<Contacto>().HasKey(c => c.Id);
            modelBuilder.Entity<Contacto>().Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Contacto>().Property(c => c.Password).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Contacto>().Property(c => c.Nombre).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Contacto>().Property(c => c.Apellidos).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Contacto>().Property(c => c.Foto).HasMaxLength(50);
            
            // IsÚnico implementation
            modelBuilder.Entity<Contacto>().Property(c => c.Login).HasMaxLength(255)
                .HasColumnAnnotation("Index", 
                new IndexAnnotation(new[]
                {
                    new IndexAttribute("Index") { IsUnique = true }
                }));

            modelBuilder.Entity<Mensaje>().ToTable("Mensaje");
            modelBuilder.Entity<Mensaje>().HasKey(m => m.Id);
            modelBuilder.Entity<Mensaje>().Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Mensaje>().Property(m => m.Asunto).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Mensaje>().Property(m => m.Contenido).IsRequired().HasMaxLength(500);

            modelBuilder.Entity<Contacto>()
            .HasMany(entity => entity.Contactos)
            .WithMany(child => child.ContactoDe)
            .Map(map =>
            {
                map.ToTable("Contacto");
                map.MapLeftKey("IdUsuario");
                map.MapRightKey("IdAmigo");
            });

            modelBuilder.Entity<Contacto>()
                .HasMany(c => c.MensajesEnviados)
                .WithOptional(m => m.Origen).Map(map => map.MapKey("IdOrigen"));

            modelBuilder.Entity<Contacto>()
                .HasMany(c => c.MensajesRecibidos)
                .WithOptional(m => m.Destino).Map(map => map.MapKey("IdDestino"));
        }
    }
}