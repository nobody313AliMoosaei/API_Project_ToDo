using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.InterfaceContext;
using ToDo.Domain.Entities;

namespace ToDo.Persistence.Context.DatabaseContext
{
    public class ApplicationDatabaseContext:IdentityDbContext<ToDo.Domain.Entities.User,Role,int>
        ,IDatabaseContext
    {
        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> option)
            :base(option)
        {
        }
        public DbSet<Card> Cards { get; set; }
    }
}
