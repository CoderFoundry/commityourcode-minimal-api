using commityourcode_minimal_api.Models;
using Microsoft.EntityFrameworkCore;

namespace commityourcode_minimal_api.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : DbContext(options)
    {        
        public required DbSet<Customer> Customers { get; set; }
      
    }
}
