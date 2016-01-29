namespace EntityFrameworkDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mensaje",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Asunto = c.String(nullable: false, maxLength: 50),
                        Contenido = c.String(nullable: false, maxLength: 500),
                        Leido = c.Boolean(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Origen_Id = c.Int(nullable: false),
                        Destino_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.Origen_Id)
                .ForeignKey("dbo.Usuario", t => t.Destino_Id)
                .Index(t => t.Origen_Id)
                .Index(t => t.Destino_Id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Apellidos = c.String(nullable: false, maxLength: 50),
                        Foto = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
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
            DropForeignKey("dbo.Mensaje", "Destino_Id", "dbo.Usuario");
            DropForeignKey("dbo.Mensaje", "Origen_Id", "dbo.Usuario");
            DropForeignKey("dbo.Contacto", "IdAmigo", "dbo.Usuario");
            DropForeignKey("dbo.Contacto", "IdUsuario", "dbo.Usuario");
            DropIndex("dbo.Contacto", new[] { "IdAmigo" });
            DropIndex("dbo.Contacto", new[] { "IdUsuario" });
            DropIndex("dbo.Mensaje", new[] { "Destino_Id" });
            DropIndex("dbo.Mensaje", new[] { "Origen_Id" });
            DropTable("dbo.Contacto");
            DropTable("dbo.Usuario");
            DropTable("dbo.Mensaje");
        }
    }
}
