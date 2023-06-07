using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.InterfaceContext;
using ToDo.Domain.Entities;

namespace ToDo.Persistence.Context.DatabaseContext
{
    public class ApplicationDatabaseContext:IdentityDbContext,IDatabaseContext
    {
        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> option)
            :base(option)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("User", "identity");
            builder.Entity<Role>().ToTable("Role", "identity");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole", "identity");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin", "identity");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim", "identity");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken", "identity");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim", "identity");

            builder.Entity<IdentityUserLogin<string>>()
                .HasKey(x => new { x.LoginProvider, x.ProviderKey });
            builder.Entity<IdentityUserToken<string>>()
                .HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
            builder.Entity<IdentityUserRole<string>>()
                .HasKey(t => new { t.RoleId, t.UserId });
        }
    }
}
