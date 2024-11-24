using Api.BusinessLogic.Mappers;
using Api.BusinessLogic.Models;
using Api.Data.Repositories.Interfaces;

namespace Api.Data.Repositories
{

    /// <summary>
    ///  Task.From Result is very BAD. Calling a non-async method from async code will result in inintended side effects
    // such as child methods running away
    /// </summary>
    public class EmployeeRepository : RepositoryBase, IEmployeeRepository
    {
        public EmployeeRepository(IDbContextAccessor dbContextAccessor) : base(dbContextAccessor) { }
        public async Task<IEnumerable<Employee>> Get()
        {
            return await Task.FromResult(DataComtext.Employees.Select(s => s.ToDomain()).AsEnumerable());
        }

        public async Task<Employee?> GetByID(int id)
        {
            var entity = DataComtext.Employees.FirstOrDefault(emp => emp.Id == id);

            return entity != null
                ? await Task.FromResult(entity.ToDomain())
                : null;
        }
    }
}
