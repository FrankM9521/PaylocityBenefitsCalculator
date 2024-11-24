using Api.BusinessLogic.Models;
using Api.BusinessLogic.Models.Response;
using Api.Data.Repositories;

namespace Api.BusinessLogic.Services
{
    public interface IAPIGet<T>
    {
        Task<IReadOnlyCollection<T>> GetAll();
        Task<T?> GetByID(int id);
    }

    public interface IAPIPost<T>
    {
        Task<CreateObjectResponse> Post(Dependent newDependent);
    }

    public interface IEmployeeService : IAPIGet<Employee> { }
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDependentRepository _dependentRepository;
        public EmployeeService(IEmployeeRepository employeeRepository, IDependentRepository dependentRepository)
        {
            _employeeRepository = employeeRepository;
            _dependentRepository = dependentRepository;
        }

        public async Task<IReadOnlyCollection<Employee>> GetAll()
        {
            var employeeResultTask = Task.Run(() => _employeeRepository.Get());
            var dependentResultTask = Task.Run(() => _dependentRepository.Get());

            await Task.WhenAll(employeeResultTask, dependentResultTask);

            var employeeResult = employeeResultTask.Result.ToDictionary(k => k.Id, v => v);
            var dependents = dependentResultTask.Result.GroupBy(d => d.EmployeeId);

            foreach (var d in dependents)
            {
                employeeResult[d.First().EmployeeId].Dependents = d.ToList();
            }

            return await Task.FromResult(employeeResult.Select(r => r.Value).ToList().AsReadOnly());
        }

        public async Task<Employee?> GetByID(int id)
        {
            var employeeResultTask = Task.Run(() => _employeeRepository.GetByID(id));
            var dependentResultTask = Task.Run(() => _dependentRepository.GetByEmployeeID(id));

            await Task.WhenAll(employeeResultTask, dependentResultTask);

            var employee = await employeeResultTask;

            if (employee != null)
            {
                employee.Dependents = await dependentResultTask ?? new List<Dependent>();
            }

            return employee;
        }
    }
}
