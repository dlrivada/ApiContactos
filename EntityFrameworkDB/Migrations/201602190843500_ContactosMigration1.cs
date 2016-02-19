namespace Infrastructure.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactosMigration1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Usuario", "Index");
            AddColumn("dbo.Usuario", "ConfirmPassword", c => c.String());
            AlterColumn("dbo.Mensaje", "Issue", c => c.String());
            AlterColumn("dbo.Mensaje", "Body", c => c.String());
            AlterColumn("dbo.Usuario", "FirstName", c => c.String());
            AlterColumn("dbo.Usuario", "LastName", c => c.String());
            AlterColumn("dbo.Usuario", "Photo", c => c.String());
            AlterColumn("dbo.Usuario", "Login", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Usuario", "Password", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.Usuario", "Login", unique: true, name: "Index");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Usuario", "Index");
            AlterColumn("dbo.Usuario", "Password", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Usuario", "Login", c => c.String(maxLength: 255));
            AlterColumn("dbo.Usuario", "Photo", c => c.String(maxLength: 50));
            AlterColumn("dbo.Usuario", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Usuario", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Mensaje", "Body", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Mensaje", "Issue", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Usuario", "ConfirmPassword");
            CreateIndex("dbo.Usuario", "Login", unique: true, name: "Index");
        }
    }
}
