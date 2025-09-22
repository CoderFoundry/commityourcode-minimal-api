using commityourcode_minimal_api.Data;
using commityourcode_minimal_api.Models;
using commityourcode_minimal_api.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace commityourcode_minimal_api.Services
{
    public class CustomerService(ApplicationDbContext context) : ICustomerService
    {
       
        public async Task<CustomerResponse?> GetCustomerByIdAsync(
            int id, CancellationToken ct = default)
        {
            var customer = await context.Customers
                 .AsNoTracking()
                 .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null) return null;

            return new CustomerResponse
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Address = customer.Address,
                Address2 = customer.Address2,
                City = customer.City,
                State = customer.State,
                ZipCode = customer.ZipCode
            };
        }

        public async Task<IEnumerable<CustomerResponse>> GetCustomersAsync(
            CancellationToken ct = default)
        {
            var customers = await context.Customers.ToListAsync();

            return customers.Select(c => new CustomerResponse
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Address = c.Address,
                Address2 = c.Address2,
                City = c.City,
                State = c.State,
                ZipCode = c.ZipCode
            });
        }

        public async Task<CustomerResponse> CreateCustomerAsync(CustomerRequest dto, CancellationToken ct)
        {
            var entity = new Customer
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Address = dto.Address,
                Address2 = dto.Address2,
                City = dto.City,
                State = dto.State,
                ZipCode = dto.ZipCode
            };

            context.Customers.Add(entity);
            await context.SaveChangesAsync(ct);

            return new CustomerResponse
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Address = entity.Address,
                Address2 = entity.Address2,
                City = entity.City,
                State = entity.State,
                ZipCode = entity.ZipCode
            };
        }
    }
    
}
