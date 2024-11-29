using Paylocity.Employees.Entities;
using Paylocity.Shared.Entities;

namespace Paylocity.Employees.DataContext.Interfaces
{
    public interface IEmployeesDbContextAccessor
    {
        //public DataBase DataBase { get; }
        List<DependentEntity> Dependents { get; }
        List<EmployeeEntity> Employees { get; }
        Task Add<T>(T model) where T : EntityBase;
    }
}
