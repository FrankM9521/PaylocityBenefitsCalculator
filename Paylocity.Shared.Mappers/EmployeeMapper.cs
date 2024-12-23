﻿using Paylocity.Employees.Entities;
using Paylocity.Employees.Models;

namespace Paylocity.Shared.Mappers.EntityDomain
{
    /*
 * Bolier Plate - This or AutoMapper
 */
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
            return new EmployeeEntity
            {
                DateOfBirth = value.DateOfBirth,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Id = value.Id,
                Salary = value.Salary,
            };
        }

        /*
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
        }*/
    }
}
