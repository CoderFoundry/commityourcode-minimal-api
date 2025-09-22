using Microsoft.AspNetCore.Http.HttpResults;
using commityourcode_minimal_api.Services;
using commityourcode_minimal_api.Models.DTO;
using commityourcode_minimal_api.Filters;


namespace commityourcode_minimal_api.Endpoints
{
    public static class CustomerEndpoints
    {
        public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder route)
        {
            var group = route.MapGroup("/api/customers")
                .WithTags("Customers")
                .AddEndpointFilter<ExceptionHandlingFilter>();
               
            
            group.MapGet("", GetCustomers)
                .WithName(nameof(GetCustomers))
                .Produces<IEnumerable<CustomerResponse>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError)
                .WithSummary("Get All Customers")
                .WithDescription("Get a list of all customers.");
                

            group.MapGet("{id:int}", GetCustomerById)
                .WithName(nameof(GetCustomerById))
                .Produces<CustomerResponse>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status500InternalServerError)
                .WithSummary("Get Customer By Id")
                .WithDescription("Get a single customer by their unique Id.");

            group.MapPost("", CreateCustomer)
                .Produces<CustomerResponse>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status500InternalServerError)
                .ProducesValidationProblem()
                .WithName(nameof(CreateCustomer))
                .WithSummary("Create a New Customer")
                .WithDescription("Create a new customer with the provided information.");

            return group;
        }

        private static async Task<Ok<IEnumerable<CustomerResponse>>> GetCustomers(ICustomerService svc, CancellationToken ct)
        {
            return TypedResults.Ok(await svc.GetCustomersAsync(ct));
        }

        private static async Task<Results<Ok<CustomerResponse>, NotFound>> GetCustomerById(int id, ICustomerService svc, CancellationToken ct)
        {
            var customer = await svc.GetCustomerByIdAsync(id, ct);

            return customer is not null 
                ? TypedResults.Ok(customer)
                : TypedResults.NotFound();
        }

        private static async Task<CreatedAtRoute<CustomerResponse>> CreateCustomer(CustomerRequest customer, ICustomerService svc, CancellationToken ct)
        {
            var createdCustomer = await svc.CreateCustomerAsync(customer, ct);

            return TypedResults.CreatedAtRoute(
                value: createdCustomer,
                routeName: nameof(GetCustomerById),
                routeValues: new { id = createdCustomer.Id }
            );
        }
    }
}