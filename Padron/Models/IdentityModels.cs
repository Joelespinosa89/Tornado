using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Padron.Models
{

    public class UserLogin : IdentityUserLogin<int>
    {
    }

    public class UserClaim : IdentityUserClaim<int>
    {
    }

    public class UserRole : IdentityUserRole<int>
    {
    }
    public class Role : IdentityRole<int, UserRole>
    {
        public Role() { }
        public Role(string name) { Name = name; }
        public string Description { get; set; }
    }
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        public Guid? UserCode { get; set; }

        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        [Display(Name = "Cédula")]
        public string Cedula { get; set; }
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
        [Required]
        public string Celular { get; set; }
        public bool Activo { get; set; }

        [ForeignKey("Rol")]
        public int? RolId { get; set; }


        public string Coordinador { get; set; }
        public string Director { get; set; }

        public virtual Role Rol { get; set; }

        [NotMapped]
        [Display(Name="Persona")]
        public string NombreCompleto { get {
                return Nombres + " " + Apellidos;
            }
        }

        [NotMapped]
        public string RolName
        {
            get
            {
                return Rol?.Name;
            }
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User,int> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            if (this.NombreCompleto?.ToString() != null)
            {
                userIdentity.AddClaim(new Claim("NombreCompleto", this.NombreCompleto?.ToString()));
            }
            if (this.UserCode?.ToString() != null)
            {
                userIdentity.AddClaim(new Claim("UserCode", this.UserCode?.ToString()));
            }
            if (this.Rol?.Name.ToString() != null)
            {
                userIdentity.AddClaim(new Claim("UserRol", this.Rol?.Name.ToString()));
            }
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<ContactForm> ContactForms { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<ElectoralCollege> ElectoralColleges { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ContactForm>().HasRequired(x => x.Municipio).WithMany().WillCascadeOnDelete(false);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);


        }
    }
}