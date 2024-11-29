using Microsoft.Extensions.DependencyInjection;
using Paylocity.Employees.DataContext;
using Paylocity.Employees.DataContext.Interfaces;
using Paylocity.Employees.Models;
using Paylocity.Employees.Repositories;
using Paylocity.Employees.Repositories.Interfaces;
using Paylocity.Employees.Services;
using Paylocity.Employees.Services.Interfaces;
using Paylocity.PayChecks.DataContext;
using Paylocity.PayChecks.DataContext.Interfaces;
using Paylocity.PayChecks.Repositoies;
using Paylocity.PayChecks.Repositories.Interfaces;
using Paylocity.PayChecks.Services;
using Paylocity.PayChecks.Services.Calculators.CalculationLibraries;
using Paylocity.PayChecks.Services.Interfaces;
using Paylocity.Shared.Config;
using Paylocity.Validation.Validation;
using Paylocity.ValidationLibrary.Interfaces;

namespace Paylocity.Employee.Api.DependencyInjectiom
{
    public static class Bootstrap
    {
            public static void RegisterConfig(this IServiceCollection services)
            {
                services.AddSingleton<IBenefitsConfig, BenefitsConfig>();
            }

            public static void RegisterDatabase(this IServiceCollection services)
            {
                services.AddScoped<IEmployeesDbContextAccessor, EmployeesDbContextAccessor>();
            }

            public static void RegisterFactories(this IServiceCollection services)
            { 
                services.AddScoped<ICalculationLibraryFactory, PayChecks.Services.CalculationLibraryFactory>();
            }
            public static void RegisterValidationCollections(this IServiceCollection services)
            {
                services.AddTransient<ILastPayCheckOfYearCalculationLibrary, LastPayCheckOfYearCalculationLibrary>();
                services.AddTransient<IStandardCalculationLibrary, StandardCalculationLibrary>();   
                services.AddTransient<IValidationCollection<Employees.Models.Employee>, EmployeeValidationCollection>();
                services.AddTransient<IValidationCollection<Dependent>, DependentValidationCollection>();
            }

            public static void RegisterRepositories(this IServiceCollection services)
            {
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
