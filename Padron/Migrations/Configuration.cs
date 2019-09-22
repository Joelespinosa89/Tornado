namespace Padron.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Padron.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Padron.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Padron.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            AddUserAndRole(context);
        }

        bool AddUserAndRole(ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<Role, int>(new RoleStore<Role, int, UserRole>(context));
            ir = rm.Create(new Role("Multiplicador") { Description = "Permite recultar personas." });
            ir = rm.Create(new Role("Sub-Cordinador") { Description = "Permite recultar personas." });
            ir = rm.Create(new Role("Cordinador") { Description = "Permite recultar personas." });
            ir = rm.Create(new Role("Director") { Description = "Permite recultar personas." });
            ir = rm.Create(new Role("Administrador") { Description = "Permite administrar el sistema en su totalidad." });


            var um = new UserManager<User, int>(
                new UserStore<User, Role, int, UserLogin, UserRole, UserClaim>(context));
            var user = new User()
            {
                //Email = "admin@oikos.com",
                UserName = "admin",
                Nombres = "Administrador",
                EmailConfirmed = true,
                Activo = true,
                Celular = "XXX-XXXXXXXX",
                RolId = rm.FindByName("Administrador").Id

            };
            ir = um.Create(user, "Padron1234");
            if (ir.Succeeded == false)
                return ir.Succeeded;
            ir = um.AddToRole(user.Id, "Administrador");

            return ir.Succeeded;
        }
    }
}
