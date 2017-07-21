namespace WebShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WebShopInitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adresas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ulica = c.String(),
                        Broj = c.Int(nullable: false),
                        Grad = c.String(),
                        Drzava = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Artikals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ime = c.String(),
                        Opis = c.String(),
                        Cena = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RaspolozivaKolicina = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stavkas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Kolicina = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cena = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Artikal_Id = c.Int(),
                        Narudzbenica_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artikals", t => t.Artikal_Id)
                .ForeignKey("dbo.Narudzbenicas", t => t.Narudzbenica_Id)
                .Index(t => t.Artikal_Id)
                .Index(t => t.Narudzbenica_Id);
            
            CreateTable(
                "dbo.Narudzbenicas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        Korisnik_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Korisniks", t => t.Korisnik_Id)
                .Index(t => t.Korisnik_Id);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Adresas", t => t.Adresa_Id)
                .Index(t => t.Adresa_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stavkas", "Narudzbenica_Id", "dbo.Narudzbenicas");
            DropForeignKey("dbo.Narudzbenicas", "Korisnik_Id", "dbo.Korisniks");
            DropForeignKey("dbo.Korisniks", "Adresa_Id", "dbo.Adresas");
            DropForeignKey("dbo.Stavkas", "Artikal_Id", "dbo.Artikals");
            DropIndex("dbo.Korisniks", new[] { "Adresa_Id" });
            DropIndex("dbo.Narudzbenicas", new[] { "Korisnik_Id" });
            DropIndex("dbo.Stavkas", new[] { "Narudzbenica_Id" });
            DropIndex("dbo.Stavkas", new[] { "Artikal_Id" });
            DropTable("dbo.Korisniks");
            DropTable("dbo.Narudzbenicas");
            DropTable("dbo.Stavkas");
            DropTable("dbo.Artikals");
            DropTable("dbo.Adresas");
        }
    }
}
