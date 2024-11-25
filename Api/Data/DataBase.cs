using Api.Data.Entities;

namespace Api.Data
{
    /*
     * Fake DB, Created as a singleton instance
     */
    public sealed class DataBase
    {
        private static readonly Lazy<DataBase> lazy = new Lazy<DataBase>(() => new DataBase());
        // since most lookups will be by employee, I am using employee id as the key for faster lookups
        private static Dictionary<int, List<DependentEntity>> _dependents;
        private static List<PayCheckEntity> _payStatements;
        private static List<EmployeeEntity> _employees;

        public static DataBase Instance => lazy.Value;

        private DataBase()
        {
            SetData(DefaultData.Employees, DefaultData.Dependents);
        }

        public void Add<T>(T entity) where T : EntityBase
        {
            switch (entity)
            {
                case EmployeeEntity emp:
                    _employees.Add(emp);
                    break;
                case PayCheckEntity payStatement:
                    _payStatements.Add(payStatement);
                    break;
                case DependentEntity dep:
                    if (_dependents.ContainsKey(dep.Id)) { _dependents[dep.Id].Add(dep); }
                    else { _dependents.Add(dep.Id, new List<DependentEntity> { dep }); }
                    break;
                default:
                    throw new Exception("Unsupported Type");
            }
        }

        public void ClearPayChecks()
        {
            if (_payStatements != null)
            {
                _payStatements.Clear();
            }
        }

        // fake data sets, read only
        public IEnumerable<EmployeeEntity> Employees
        {
            get { return (_employees ??= new List<EmployeeEntity>()).AsReadOnly(); }
        }

        public IEnumerable<DependentEntity> Dependents
        {
            get { return (_dependents ?? (_dependents = new Dictionary<int, List<DependentEntity>>())).SelectMany(v => v.Value).ToList().AsReadOnly(); }
        }

        public IEnumerable<PayCheckEntity> PayStatements
        {
            get { return (_payStatements ?? (_payStatements = new List<PayCheckEntity>())).AsReadOnly(); }
        }

        public void SetData(List<EmployeeEntity> employees, List<DependentEntity> dependents)
        {
            _employees = employees;
            _dependents = dependents.GroupBy(d => d.EmployeeId).ToDictionary(k => k.Key, v => v.ToList());
        }
    }
}
