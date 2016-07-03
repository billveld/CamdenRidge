namespace CamdenRidge.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
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


            context.Roles.AddOrUpdate(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Id = "Admin", Name = "Admin" });
            context.Roles.AddOrUpdate(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Id = "Board Member", Name = "Board Member" });
            context.Roles.AddOrUpdate(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Id = "AECC Member", Name = "AECC Member" });

            var user = new ApplicationUser
            {
                Id = "f1aa5d2d-f3e4-4fc3-ac1e-fcb401102693",
                Name = "Bill Veldman",
                UserName = "bill.veldman@gmail.com",
                Email = "bill.veldman@gmail.com",
                Address = "240 Leasingworth Way",
                PasswordHash = "AA5jSWQjIEK4P5ZQU2nNXADAsx6XBbYR/ae2Dee5F9zjqMXeR06xhl+urZxDuLLalQ==",
                SecurityStamp = "1bb5f277-4aef-486a-a791-d62582f23974",
                TwoFactorEnabled = false,
                EmailConfirmed = false,
                LockoutEnabled = false,
                PhoneNumberConfirmed = false
            
            };

            user.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole { RoleId = "Admin", UserId = "f1aa5d2d-f3e4-4fc3-ac1e-fcb401102693" });

            context.Users.AddOrUpdate(user);
            context.SaveChanges();

        }
    }
}
