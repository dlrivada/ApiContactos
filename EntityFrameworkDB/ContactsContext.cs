using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Domain.Model.ContactAggregate;

namespace Infrastructure.EntityFramework
{
    public class ContactsContext : DbContext
    {
        // Your context has been configured to use a 'Model1' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'EntityFrameworkDB.Model1' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model1' 
        // connection string in the application configuration file.
        public ContactsContext() : base("name=Model1")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Contact> User { get; set; }
        public virtual DbSet<Message> Message { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Configuration.LazyLoadingEnabled = false;
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Contact>().ToTable("Usuario");
            modelBuilder.Entity<Contact>().HasKey(c => c.Id);
            modelBuilder.Entity<Contact>().Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // IsUnique implementation
            //modelBuilder.Entity<Contact>().Property(c => c.Login)
            //    .HasColumnAnnotation("Index",
            //    new IndexAnnotation(new[]
            //    {
            //        new IndexAttribute("Index") { IsUnique = true }
            //    }));

            modelBuilder.Entity<Message>().ToTable("Mensaje");
            modelBuilder.Entity<Message>().HasKey(m => m.Id);
            modelBuilder.Entity<Message>().Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Contact>()
            .HasMany(entity => entity.Contacts)
            .WithMany(child => child.ContactsFrom)
            .Map(map =>
            {
                map.ToTable("Contacto");
                map.MapLeftKey("IdUsuario");
                map.MapRightKey("IdAmigo");
            });

            modelBuilder.Entity<Contact>()
                .HasMany(c => c.MessagesSended)
                .WithOptional(m => m.From)
                .Map(map => map.MapKey("IdOrigen"));

            modelBuilder.Entity<Contact>()
                .HasMany(c => c.MessagesReceived)
                .WithOptional(m => m.To)
                .Map(map => map.MapKey("IdDestino"));
        }
    }
}