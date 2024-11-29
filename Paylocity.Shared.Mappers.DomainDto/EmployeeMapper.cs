using Paylocity.Employees.Dtos.Employee;
using Paylocity.Employees.Models;

namespace Paylocity.Shared.Mappers.DomainDto
{
    /*
 * Bolier Plate - This or AutoMapper
 */
    public static class EmployeeMapper
    {
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
