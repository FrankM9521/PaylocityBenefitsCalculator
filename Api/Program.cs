using Microsoft.OpenApi.Models;
using MediatR;
using System.Reflection;
using Api.BusinessLogic.Models;
using Api.Data.Repositories;
using Api.BusinessLogic.Services;
using Api.BusinessLogic.Validation;
using Api.Data;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Employee Benefit Cost Calculation Api",
                Description = "Api to support employee benefit cost calculations"
            });
        });

        var allowLocalhost = "allow localhost";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(allowLocalhost,
                policy => { policy.WithOrigins("http://localhost:3000", "http://localhost"); });
        });

        builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
        builder.Services.AddScoped<IDbContextAccessor, DbContextAccessor>();
        builder.Services.AddTransient<IValidationCollection<Employee>, EmployeeValidationCollection>();
        builder.Services.AddTransient<IValidationCollection<Dependent>, DependentValidationCollection>();
        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddScoped<IEmployeeService, EmployeeService>();
        builder.Services.AddScoped<IDependentRepository, DependentRepository>();
        builder.Services.AddScoped<IDependentService, DependentService>();
        builder.Services.AddTransient<ICalculatePayrollService, CalculatePayrollService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(allowLocalhost);

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}