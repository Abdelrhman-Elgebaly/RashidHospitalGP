using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using Hospital.DAL;

namespace RashidHospital.Models
{
    public class UserRole : IdentityUserRole<Guid>
    {
    }

    public class UserClaim : IdentityUserClaim<Guid>
    {
    }

    public class UserLogin : IdentityUserLogin<Guid>
    {
    }
    public class Role : IdentityRole<Guid, UserRole>
    {
        public Role()
        {
            Id = Guid.NewGuid();
        }
        public Role(string name) { Name = name; }
    }

    public class UserStore : UserStore<ApplicationUser, Role, Guid,
        UserLogin, UserRole, UserClaim>
    {
        public UserStore(ApplicationDbContext context) : base(context)
        {
        }
    }

    public class RoleStore : RoleStore<Role, Guid, UserRole>
    {
        public RoleStore(ApplicationDbContext context) : base(context)
        {
        }
    }
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<Guid, UserLogin, UserRole, UserClaim>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
        }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public System.Guid CreatedBy { get; set; }
        public System.Guid Modifiedby { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {

            if (string.IsNullOrEmpty(this.SecurityStamp))
            {
                this.SecurityStamp = Guid.NewGuid().ToString().Replace("-", "");
            }
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, Guid,
        UserLogin, UserRole, UserClaim>
    {
        public ApplicationDbContext()
            : base("Model1")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }



    }
}