using Microsoft.EntityFrameworkCore;

namespace Demo.Models
{
    public class ITIEntity : DbContext
    {
        public ITIEntity() { }
        public ITIEntity(DbContextOptions options )  : base (options) { }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MR-ARRAY;Initial Catalog=WebApiDemo;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
