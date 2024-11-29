using Paylocity.Employees.Entities;
using Paylocity.Shared.Entities;

namespace Paylocity.Employees.DataContext
{
    /*
     * Fake DB, Created as a singleton instance
     */
    public sealed class DataBase
    {
        private static readonly Lazy<DataBase> lazy = new Lazy<DataBase>(() => new DataBase());
        // since most lookups will be by employee, I am using employee id as the key for faster lookups
        private static List<EmployeeEntity> _employees;
        private static List<DependentEntity> _dependents;
        public static DataBase Instance => lazy.Value;

        private DataBase()
        {
            SetData(DefaultData.Employees, DefaultData.Dependents);
        }

        public async Task Add<T>(T entity) where T : EntityBase
        {
            switch (entity)
            {
                case EmployeeEntity emp:
                    _employees.Add(emp);
                    break;
                case DependentEntity dep:
                    _dependents.Add(dep);
                    break;
                default:
                    throw new Exception("Unsupported Type");
            }
        }

        // fake data sets, read only
        public IEnumerable<EmployeeEntity> Employees
        {
            get { return (_employees ??= new List<EmployeeEntity>()).AsReadOnly(); }
        }

        public IEnumerable<DependentEntity> Dependents
        {
            get { return _dependents ?? (_dependents = new List<DependentEntity>()); }
        }

        public void SetData(List<EmployeeEntity> employees, List<DependentEntity> dependents)
        {
            _employees = employees;
            _dependents = dependents;
        }
    }
}
