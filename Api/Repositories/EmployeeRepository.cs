using Api.Mappers;
using Api.Models;

namespace Api.Repositories
{
    public interface IEmployeeRepository : IGetRepositoryBase<Employee> { }

    /// <summary>
    ///  Task.From Result is very BAD. Calling a non-async method from async code will result in inintended side effects
    // such as child methods running away
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        public async Task<IEnumerable<Employee>> Get()
        {
           return await Task.FromResult(DB.Employees.Select(s => s.ToDomain()).AsEnumerable());
        }

        public async Task<Employee?> GetByID(int id)
        {
            var entity = DB.Employees.FirstOrDefault(emp => emp.Id == id);

            return entity != null
                ? await Task.FromResult(entity.ToDomain())
                : null;
        }

        private DataBase DB
        {
            get { return DataBase.Instance; }   
        }
    }
}
