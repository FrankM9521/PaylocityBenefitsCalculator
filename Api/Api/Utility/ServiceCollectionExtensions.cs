using Api.BusinessLogic.Models;
using Api.BusinessLogic.Validation;
using Api.Data.Repositories.Interfaces;
using Api.Data.Repositories;
using Api.BusinessLogic.Services;

namespace Api.Api.Utility
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterValidationCollections(this IServiceCollection services)
        {
            services.AddTransient<IValidationCollection<Employee>, EmployeeValidationCollection>();
            services.AddTransient<IValidationCollection<Dependent>, DependentValidationCollection>();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDependentRepository, DependentRepository>();
            services.AddScoped<IPayStatementRepository, PayStatementRepository>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDependentService, DependentService>();
            services.AddScoped<ICalculatePayrollService, CalculatePayrollService>();
        }
    }
}
