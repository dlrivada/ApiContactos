namespace Infrastructure.EntityFramework.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ContactosMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mensaje",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Issue = c.String(nullable: false, maxLength: 50),
                        Body = c.String(nullable: false, maxLength: 500),
                        Readed = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        IdDestino = c.Int(),
                        IdOrigen = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.IdDestino)
                .ForeignKey("dbo.Usuario", t => t.IdOrigen)
                .Index(t => t.IdDestino)
                .Index(t => t.IdOrigen);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Photo = c.String(maxLength: 50),
                        Login = c.String(maxLength: 255),
                        Password = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Login, unique: true, name: "Index");
            
            CreateTable(
                "dbo.Contacto",
                c => new
                    {
                        IdUsuario = c.Int(nullable: false),
                        IdAmigo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdUsuario, t.IdAmigo })
                .ForeignKey("dbo.Usuario", t => t.IdUsuario)
                .ForeignKey("dbo.Usuario", t => t.IdAmigo)
                .Index(t => t.IdUsuario)
                .Index(t => t.IdAmigo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mensaje", "IdOrigen", "dbo.Usuario");
            DropForeignKey("dbo.Mensaje", "IdDestino", "dbo.Usuario");
            DropForeignKey("dbo.Contacto", "IdAmigo", "dbo.Usuario");
            DropForeignKey("dbo.Contacto", "IdUsuario", "dbo.Usuario");
            DropIndex("dbo.Contacto", new[] { "IdAmigo" });
            DropIndex("dbo.Contacto", new[] { "IdUsuario" });
            DropIndex("dbo.Usuario", "Index");
            DropIndex("dbo.Mensaje", new[] { "IdOrigen" });
            DropIndex("dbo.Mensaje", new[] { "IdDestino" });
            DropTable("dbo.Contacto");
            DropTable("dbo.Usuario");
            DropTable("dbo.Mensaje");
        }
    }
}
