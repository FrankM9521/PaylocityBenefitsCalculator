using Api.Dtos.Employee;
using Api.Enitiies;
using Api.Models;

namespace Api.Mappers
{
    /// <summary>
    /// This is option B for mapping entities  to domain objects, domain to dto, etc.
    /// I would have used automapper, but I am working off a recent setup of Visual Studio 2022
    /// and am having issues installing a correct package for .NET 6
    /// 
    /// I do use this a lot, especially in an environment where the back end supports both .NET Framework and Core
    /// You need to configure in the start up of one or the other, have not found a way to make it work with both
    /// 
    /// I use a lot of static mappers to keep assignment out of  code statements to make it much easier to understand.
    /// It looks like a lot of work, but intellisense nakes it fast
    /// </summary>
    public static class EmployeeMapper
    {
        public static Employee ToDomain(this EmployeeEntity value)
        {
            return new Employee
            {
                DateOfBirth = value.DateOfBirth,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Id = value.Id,
                Salary = value.Salary,
                Dependents = value.Dependents.Select(d => d.ToDomain()).ToList()
            };
        }

        public static EmployeeEntity ToEntity(this Employee value)
        {
            return new Enitiies.EmployeeEntity
            {
                DateOfBirth = value.DateOfBirth,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Id = value.Id,
                Salary = value.Salary,
            };
        }

        public static GetEmployeeDto ToDto(this Employee value)
        {
            return new GetEmployeeDto
            {
                DateOfBirth = value.DateOfBirth,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Id = value.Id,
                Salary = value.Salary,
                Dependents = value.Dependents.Select(d => d.ToDto()).ToList()
            };
        }
    }
}
