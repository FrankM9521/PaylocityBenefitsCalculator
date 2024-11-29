using Paylocity.Employees.DataContext.Interfaces;
using Paylocity.Employees.Entities;
using Paylocity.Shared.Entities;

namespace Paylocity.Employees.DataContext
{
    public class EmployeesDbContextAccessor : IEmployeesDbContextAccessor
    {
        public List<DependentEntity> Dependents { get => DataBase.Dependents.ToList(); }
        public List<EmployeeEntity> Employees { get => DataBase.Employees.ToList(); }

        public async Task Add<T>(T entity) where T : EntityBase
        {
            await DataBase.Add(entity);
        }

        private EmployeesDataContext DataBase
        {
            get
            {
                return EmployeesDataContext.Instance;
            }
        }
    }
}
