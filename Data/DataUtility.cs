using Bogus;
using commityourcode_minimal_api.Models;
using Microsoft.EntityFrameworkCore;

namespace commityourcode_minimal_api.Data
{
  public static class DataUtility
  {
    public static async Task ManageDataAsync(IServiceProvider svcProvider)
    {
      await using var dbContextSvc = svcProvider.GetRequiredService<ApplicationDbContext>();

      await dbContextSvc.Database.MigrateAsync();

      await SeedDemoCustomersAsync(dbContextSvc);      
    }

    public static async Task SeedDemoCustomersAsync(ApplicationDbContext context, int count = 50)
    {
      try
      {
        if (context.Customers.Any())
        {
          // If customers already exist, skip seeding
          return;
        }
        // Generate fake customers using Bogus
        var customers = GenerateCustomers(count);
        context.Customers.AddRange(customers);
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        Console.WriteLine("*************  ERROR  *************");
        Console.WriteLine("Error Seeding Customers.");
        Console.WriteLine(ex.Message);
        Console.WriteLine("***********************************");
        throw;
      }
    }

 
    /// <summary>
    /// Generates a list of fake Customer entities.
    /// </summary>
    /// <param name="count">Number of customers to generate.</param>
    public static List<Customer> GenerateCustomers(int count)
    {
      var id = 1;
      var customerFaker = new Faker<Customer>()
          // incrementing index for Id
          .RuleFor(c => c.Id, f => id++)
          .RuleFor(c => c.FirstName, f => f.Name.FirstName())
          .RuleFor(c => c.LastName, f => f.Name.LastName())
          .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.FirstName, c.LastName))
          .RuleFor(c => c.Address, f => f.Address.StreetAddress())
          .RuleFor(c => c.Address2, f => f.Random.Bool(0.2f) ? f.Address.SecondaryAddress() : null)
          .RuleFor(c => c.City, f => f.Address.City())
          .RuleFor(c => c.State, f => f.Address.StateAbbr())
          .RuleFor(c => c.ZipCode, f => f.Address.ZipCode("#####"));

      return customerFaker.Generate(count);
    }

   
  }

}
