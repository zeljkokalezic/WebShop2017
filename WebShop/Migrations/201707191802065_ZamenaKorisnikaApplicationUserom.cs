namespace WebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ZamenaKorisnikaApplicationUserom : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Korisniks", "Adresa_Id", "dbo.Adresas");
            DropForeignKey("dbo.Narudzbenicas", "Korisnik_Id", "dbo.Korisniks");
            DropIndex("dbo.Narudzbenicas", new[] { "Korisnik_Id" });
            DropIndex("dbo.Korisniks", new[] { "Adresa_Id" });
            AddColumn("dbo.Narudzbenicas", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Adresa_Id", c => c.Int());
            CreateIndex("dbo.Narudzbenicas", "User_Id");
            CreateIndex("dbo.AspNetUsers", "Adresa_Id");
            AddForeignKey("dbo.AspNetUsers", "Adresa_Id", "dbo.Adresas", "Id");
            AddForeignKey("dbo.Narudzbenicas", "User_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Narudzbenicas", "Korisnik_Id");
            DropTable("dbo.Korisniks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Korisniks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ime = c.String(),
                        Prezime = c.String(),
                        Email = c.String(),
                        BrojTelefona = c.String(),
                        Adresa_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Narudzbenicas", "Korisnik_Id", c => c.Int());
            DropForeignKey("dbo.Narudzbenicas", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Adresa_Id", "dbo.Adresas");
            DropIndex("dbo.AspNetUsers", new[] { "Adresa_Id" });
            DropIndex("dbo.Narudzbenicas", new[] { "User_Id" });
            DropColumn("dbo.AspNetUsers", "Adresa_Id");
            DropColumn("dbo.Narudzbenicas", "User_Id");
            CreateIndex("dbo.Korisniks", "Adresa_Id");
            CreateIndex("dbo.Narudzbenicas", "Korisnik_Id");
            AddForeignKey("dbo.Narudzbenicas", "Korisnik_Id", "dbo.Korisniks", "Id");
            AddForeignKey("dbo.Korisniks", "Adresa_Id", "dbo.Adresas", "Id");
        }
    }
}
