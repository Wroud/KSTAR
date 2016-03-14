using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using KSTAR.Models;

namespace KSTAR.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }
        public DbSet<FPost> ForumUser { get; set; }
        public DbSet<FGroup> ForumGroup { get; set; }
        public DbSet<FSubject> ForumSubject { get; set; }
        public DbSet<FTopic> ForumTopic { get; set; }
        public DbSet<FPost> ForumPost { get; set; }

        public void EnsureSeedData()
        {
            if (!ApplicationRole.Any())
            {
                var administrator = ApplicationRole.Add(new ApplicationRole() { Name = BaseRoles.Administrator.ToString(), NormalizedName = BaseRoles.Administrator.ToString().ToUpper() }).Entity;
                var user = ApplicationRole.Add(new ApplicationRole() { Name = BaseRoles.User.ToString(), NormalizedName = BaseRoles.User.ToString().ToUpper() }).Entity;

                RoleClaims.AddRange(
                    new IdentityRoleClaim<string>() { ClaimType = "Banned", ClaimValue = "false", RoleId = administrator.Id },
                    new IdentityRoleClaim<string>() { ClaimType = "Dashboard", ClaimValue = "true", RoleId = administrator.Id },
                    new IdentityRoleClaim<string>() { ClaimType = "Banned", ClaimValue = "false", RoleId = user.Id });
                SaveChanges();
            }
        }
    }
}
