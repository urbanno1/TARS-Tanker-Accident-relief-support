using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;

namespace TARS.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisteredDate { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
        {
        public ApplicationDbContext()
            :base("DefaultConnection", throwIfV1Schema:false)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //AspnetUser -> User
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("User");
            //AspNetRole -> Role
            modelBuilder.Entity<IdentityRole>()
                .ToTable("Role");
            //AspNetUserRole -> UserRole
            modelBuilder.Entity<IdentityUserRole>()
                .ToTable("UserRole");
            //AspNetUserCLaims -> UserClaima
            modelBuilder.Entity<IdentityUserClaim>()
                .ToTable("UserClaim");
            //AspNetUserLogins -> UserLogin
            modelBuilder.Entity<IdentityUserLogin>()
                .ToTable("UserLogin");
        }
    }
}