using Api.Models;
using Api.Repositories;

namespace Api.Services
{
    public interface IAPIGet<T>
    {
        Task<IReadOnlyCollection<T>> GetAll();
        Task<T?> GetByID(int id);
    }

    public interface IAPIPost<T>
    {
        Task<CreateResponse> Post(Dependent newDependent);
    }

    public interface IEmployeeService : IAPIGet<Employee>{ }
    public class EmployeeService :  IEmployeeService
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

            Task.WaitAll(employeeResultTask, dependentResultTask);

            var employeeResult = employeeResultTask.Result.ToDictionary(k => k.Id, v => v);
            var dependents = dependentResultTask.Result.GroupBy(d => d.EmployeeId);

            foreach (var d in dependents)
            {
                employeeResult[d.First().EmployeeId].Dependents = d.ToList();
            }

            return employeeResult.Select(r => r.Value).ToList().AsReadOnly();
        }

        public async Task<Employee?> GetByID(int id)
        {
            var result = await _employeeRepository.GetByID(id);

            return result;
        }
    }
}
