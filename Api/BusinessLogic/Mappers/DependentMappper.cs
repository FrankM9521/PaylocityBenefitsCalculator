using Api.Api.Dtos.Dependent;
using Api.BusinessLogic.Models;
using Api.Data.Entities;

namespace Api.BusinessLogic.Mappers
{
    public static class DependentMappper
    {
        public static Dependent ToDomain(this DependentEntity value)
        {
            return new Dependent
            {
                DateOfBirth = value.DateOfBirth,
                EmployeeId = value.EmployeeId,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Id = value.Id,
                Relationship = (Relationship)value.Relationship
            };
        }

        public static DependentEntity ToEntity(this Dependent value)
        {
            return new DependentEntity
            {
                DateOfBirth = value.DateOfBirth,
                EmployeeId = value.EmployeeId,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Id = value.Id,
                Relationship = (RelationshipType)value.Relationship
            };
        }

        public static GetDependentDto ToDto(this Dependent value)
        {
            return new GetDependentDto
            {
                DateOfBirth = value.DateOfBirth,
                EmployeeId = value.EmployeeId,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Id = value.Id,
                Relationship = value.Relationship
            };
        }

        public static Dependent ToModel(this GetDependentDto value)
        {
            return new Dependent
            {
                DateOfBirth = value.DateOfBirth,
                EmployeeId = value.EmployeeId,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Relationship = value.Relationship
            };
        }
    }
}
