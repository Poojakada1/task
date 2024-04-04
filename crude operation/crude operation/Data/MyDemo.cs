using crude_operation.Models;
using Microsoft.EntityFrameworkCore;

namespace crude_operation.Data
{
    public class MyDemo : DbContext
    {
        public MyDemo(DbContextOptions option) : base(option) 
        {
        
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
