using Paylocity.Employees.Entities;
using Paylocity.Employees.Models;

namespace Paylocity.Shared.Mappers.EntityDomain
{
    /*
     * Bolier Plate - This or AutoMapper
     */
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
    }
}
