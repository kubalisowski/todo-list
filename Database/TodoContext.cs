using Database.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class TodoContext : DbContext
    {
        public TodoContext()
        { }
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        { }

        public DbSet<Item> Item { get; set; }
        public DbSet<Logs> Log { get; set; }
    }
}
