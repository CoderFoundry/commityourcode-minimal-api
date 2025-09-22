using commityourcode_minimal_api.Models.DTO;

namespace commityourcode_minimal_api.Services
{
    public interface ICustomerService
    {

        Task<IEnumerable<CustomerResponse>> GetCustomersAsync(CancellationToken ct);

        Task<CustomerResponse?> GetCustomerByIdAsync(int id, CancellationToken ct);

        Task<CustomerResponse> CreateCustomerAsync(CustomerRequest customer, CancellationToken ct);

    }
}
