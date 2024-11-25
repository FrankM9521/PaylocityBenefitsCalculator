using Api.BusinessLogic.Models;
using Api.BusinessLogic.Validation;
using Api.Data.Repositories.Interfaces;
using Api.Data.Repositories;
using Api.BusinessLogic.Services;
using Api.BusinessLogic.Services.Interfaces;
using Api.Data;
using Api.BusinessLogic.Factories;

namespace Api.Api.Utility
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterConfig(this IServiceCollection services)
        {
            services.AddSingleton<IBenefitsConfig, BenefitsConfig>();
        }

        public static void RegisterDatabase(this IServiceCollection services)
        {
            services.AddScoped<IDbContextAccessor, DbContextAccessor>();
        }

        public static void RegisterFactories(this IServiceCollection services)
        {
            services.AddScoped<ICalculationLibraryFactory, CalculationLibraryFactory>();
        }
        public static void RegisterValidationCollections(this IServiceCollection services)
        {
            services.AddTransient<IValidationCollection<Employee>, EmployeeValidationCollection>();
            services.AddTransient<IValidationCollection<Dependent>, DependentValidationCollection>();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDependentRepository, DependentRepository>();
            services.AddScoped<IPayCheckRepository, PayCheckRepository>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDependentService, DependentService>();
            services.AddScoped<ICalculatePayCheckService, CalculatePayCheckService>();
            services.AddScoped<IPayCheckService, PayCheckService>();
        }
    }
}
