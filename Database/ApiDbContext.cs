using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Database
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions option):base(option){

        }

        public DbSet<Todo> Todos { get; set; }
    }
}