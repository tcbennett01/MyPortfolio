namespace tbPortfolio.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<tbPortfolio.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(tbPortfolio.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(
            new RoleStore<IdentityRole>(context));
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Moderator"))
            {
                roleManager.Create(new IdentityRole { Name = "Moderator" });
            }

            var userManager = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(context));
            if (!context.Users.Any(u => u.Email == "tcbenett01@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "tcbennett01@gmail.com",
                    Email = "tcbennett01@gmail.com",
                    FirstName = "Tim",
                    LastName = "Bennett",
                    DisplayName = "Tim"
                }, "initialP@ssword!");
            }

            if (!context.Users.Any(u => u.Email == "moderator@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "moderator@coderfoundry.com",
                    Email = "moderator@coderfoundry.com",
                    FirstName = "",
                    LastName = "",
                    DisplayName = "Moderator"
                }, "initialP@ssword!");
            }

            var userId = userManager.FindByEmail("tcbennett01@gmail.com").Id;
            userManager.AddToRole(userId, "Admin");

            var muserId = userManager.FindByEmail("moderator@coderfoundry.com").Id;
            userManager.AddToRole(muserId, "Moderator");
        }
    }
}
