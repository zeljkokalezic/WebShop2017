namespace WebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImeIPrezime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Ime", c => c.String());
            AddColumn("dbo.AspNetUsers", "Prezime", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Prezime");
            DropColumn("dbo.AspNetUsers", "Ime");
        }
    }
}
