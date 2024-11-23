using Api.Enitiies;
using Api.Entities;
using Api.Models;

namespace Api.Repositories
{
    /*
     * Fake DB, Created as a singleton instance
     */
    public sealed class DataBase
    {
        private static readonly Lazy<DataBase> lazy = new Lazy<DataBase>(() => new DataBase());
        // since most lookups will be by employee, I am using employee id as the key for fater lookups
        private static Dictionary<int, List<DependentEntity>> _dependents;
        private static List<PayStatementEntity> _payStatements;
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
                case PayStatementEntity payStatement:
                    _payStatements.Add(payStatement);
                    break;
                case DependentEntity dep:
                    if (_dependents.ContainsKey(dep.Id))   {  _dependents[dep.Id].Add(dep);  }
                    else { _dependents.Add(dep.Id, new List<DependentEntity> { dep }); }
                    break;
                default:
                    throw new Exception("Unsupported Type");
            }
        }

        // fake data sets, read only
        public IReadOnlyList<EmployeeEntity> Employees
        {
            get { return (_employees ??= new List<EmployeeEntity>()).AsReadOnly();  } 
        }

        public IReadOnlyList<DependentEntity> Dependents
        {
            get {  return ( _dependents ?? ( _dependents = new Dictionary<int, List<DependentEntity>>())).SelectMany(v => v.Value).ToList().AsReadOnly();  } 
        }

        public IReadOnlyList<PayStatementEntity> PayStatements
        {
            get { return (_payStatements ?? (_payStatements = new List<PayStatementEntity>())).AsReadOnly(); }
        }

        public  void SetData(List<EmployeeEntity> employees, List<DependentEntity> dependents)
        {
            _employees = employees;
            _dependents = dependents.GroupBy(d => d.EmployeeId).ToDictionary(k => k.Key, v => v.ToList());
        }
    }
   
    public static class DefaultData
    {
        public static List<EmployeeEntity> Employees
        {
            get
            {
                return new List<EmployeeEntity>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "LeBron",
                        LastName = "James",
                        Salary = 75420.99m,
                        DateOfBirth = new DateTime(1984, 12, 30)
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Ja",
                        LastName = "Morant",
                        Salary = 92365.22m,
                        DateOfBirth = new DateTime(1999, 8, 10),
                    },
                    new()
                    {
                        Id = 3,
                        FirstName = "Michael",
                        LastName = "Jordan",
                        Salary = 143211.12m,
                        DateOfBirth = new DateTime(1963, 2, 17)
                    }
                };
            }
        }

        public static List<DependentEntity> Dependents
        { 
            get
            {
                return new List<DependentEntity>
                {
                    new()
                    {
                        Id = 1,
                        FirstName = "Spouse",
                        LastName = "Morant",
                        Relationship = (RelationshipType)Relationship.Spouse,
                        DateOfBirth = new DateTime(1998, 3, 3),
                        EmployeeId = 2
                    },
                    new()
                    {
                        Id = 2,
                        FirstName = "Child1",
                        LastName = "Morant",
                        Relationship = (RelationshipType)Relationship.Child,
                        DateOfBirth = new DateTime(2020, 6, 23),
                        EmployeeId= 2
                    },
                    new()
                    {
                        Id = 3,
                        FirstName = "Child2",
                        LastName = "Morant",
                        Relationship = (RelationshipType)Relationship.Child,
                        DateOfBirth = new DateTime(2021, 5, 18),
                        EmployeeId = 2
                    },
                    new()
                    {
                        Id = 4,
                        FirstName = "DP",
                        LastName = "Jordan",
                        Relationship =(RelationshipType)Relationship.DomesticPartner,
                        DateOfBirth = new DateTime(1974, 1, 2),
                        EmployeeId = 3
                    }
                };
            }
            } 
    }
}
