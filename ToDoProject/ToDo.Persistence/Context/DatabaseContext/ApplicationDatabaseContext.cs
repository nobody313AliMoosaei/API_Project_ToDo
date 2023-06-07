using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.Entities;

namespace ToDo.Persistence.Context.DatabaseContext
{
    public class ApplicationDatabaseContext:IdentityDbContext
    {
        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> option)
            :base(option)
        {
        }

        public DbSet<User> Users { get; set; }

    }
}
