namespace WebShop.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using System.Web.Configuration;

    internal sealed class Configuration : DbMigrationsConfiguration<WebShop.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            //ukljucujemo automatske migracije
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(WebShop.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //create roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if( ! roleManager.RoleExists(Roles.KUPAC))
            {
                roleManager.Create(new IdentityRole(Roles.KUPAC));
            }

            if ( ! roleManager.RoleExists(Roles.ADMIN))
            {
                roleManager.Create(new IdentityRole(Roles.ADMIN));
            }

            var adminEmail = WebConfigurationManager.AppSettings["AdminEmail"];

            if ( ! context.Users.Any(x => x.Email == adminEmail))
            {
                //admin menadzer nam sluzi za rad sa korisnicima
                var adminManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                //ovde dobijemo password hash
                var password = WebConfigurationManager.AppSettings["AdminPassword"];
                var passwordHash = adminManager.PasswordHasher.HashPassword(password);

                //kreiramo novog administratora
                var admin = new ApplicationUser
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    Ime = WebConfigurationManager.AppSettings["AdminName"],
                    PasswordHash = passwordHash,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                //snimamo ga u bazu
                context.Users.Add(admin);
                context.SaveChanges();

                //dodeljujemo rolu administrator
                adminManager.AddToRole(admin.Id, Roles.ADMIN);
            }
        }
    }
}
