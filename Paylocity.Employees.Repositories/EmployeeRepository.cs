using Paylocity.Employees.DataContext.Interfaces;
using Paylocity.Employees.Entities;
using Paylocity.Employees.Models;
using Paylocity.Employees.Repositories.Interfaces;
using Paylocity.Shared.Mappers.EntityDomain;

namespace Paylocity.Employees.Repositories
{

    /// <summary>
    ///  Task.From Result is very BAD. Calling a non-async method from async code will result in inintended side effects
    // such as child methods running away
    /// </summary>
    public class EmployeeRepository :  IEmployeeRepository
    {
        private readonly IEmployeesDbContextAccessor _employeesDbContextAccessor;
        public EmployeeRepository(IEmployeesDbContextAccessor dbContextAccessor)
        {
            _employeesDbContextAccessor = dbContextAccessor;
        }
        public async Task<IEnumerable<Employee>> Get()
        {
            return await Task.FromResult(_employeesDbContextAccessor.Employees.Select(s => s.ToDomain()).AsEnumerable());
        }

        public async Task<Employee?> GetByID(int id)
        {
            var entity = _employeesDbContextAccessor.Employees.FirstOrDefault(emp => emp.Id == id);

            return entity != null
                ? await Task.FromResult(entity.ToDomain())
                : null;
        }
    }
}
