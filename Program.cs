using commityourcode_minimal_api.Extensions;
using Microsoft.EntityFrameworkCore;
using commityourcode_minimal_api.Data;
using commityourcode_minimal_api.Services;
using commityourcode_minimal_api.Endpoints.Customer;


var builder = WebApplication.CreateBuilder(args);

//custom extension method to configure open api. so that program.cs is cleaner
builder.Services.ConfigureOpenApi();

//create a sql lite db in the root folder
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=MinApiDemo.db"));

builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddValidation();

var app = builder.Build();

using (var  scope = app.Services.CreateScope())
{
    await DataUtility.ManageDataAsync(scope.ServiceProvider);
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.UseStaticFiles();
app.MapScalar();

app.MapCustomerEndpoints();


app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
