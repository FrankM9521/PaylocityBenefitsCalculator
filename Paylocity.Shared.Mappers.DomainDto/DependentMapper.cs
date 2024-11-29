using Paylocity.Employees.Dtos.Dependent;
using Paylocity.Employees.Models;

namespace Paylocity.Shared.Mappers.DomainDto
{
    public static class DependentMapper
    {
        public static GetDependentDto ToDto(this Dependent value)
        {
            return new GetDependentDto
            {
                 Relationship = value.Relationship,
                 DateOfBirth = value.DateOfBirth,
                 EmployeeId = value.EmployeeId,
                 FirstName = value.FirstName,
                 LastName = value.LastName,
                 Id = value.Id,
            };
        }

        public static Dependent ToModel(this GetDependentDto value)
        {
            return new Dependent
            {
                Relationship = value.Relationship,
                DateOfBirth = value.DateOfBirth,
                EmployeeId = value.EmployeeId,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Id = value.Id,
            };
        }
    }
}
