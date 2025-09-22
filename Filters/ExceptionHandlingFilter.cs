namespace commityourcode_minimal_api.Filters
{
    public class ExceptionHandlingFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
        {
            try
            {
                return await next(context); // Run the actual endpoint
            }
            catch (Exception ex)
            {
                // Loging the exception code would go here (you can use any logging framework)

                // Standardized Problem response
                return Results.Problem(
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: "An unexpected error occurred");
            }
        }
    }
}
