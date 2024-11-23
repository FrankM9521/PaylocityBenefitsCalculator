using Api.Models;
using System;
using System.Collections.Generic;

namespace ApiTests.UnitTests
{
    public partial class PayStatementCalculations_Tests
    {
        public static class TestHelpers
        {
            public static PayStatement TestPayStatement(decimal salary, int numberOfDependents = 0, int age = 34)
            {
                var emp = TestEmployee(1, salary, numberOfDependents, age);

                return new PayStatement
                {
                    Employee = emp,
                    GrossPay = emp.Salary / 26
                };
            }
            public static Employee TestEmployee(int empId, decimal salary, int numberOfDepenedents = 0, int age = 34)
            {
                var names = new string[] { "Marge", "Lisa", "Bart", "Maggie", "Lenny", "Carl", "Milhouse", "Santas Little Helper", "Nelson", "Otto" };
                var ages = new int[] { 32, 8, 10, 3, 33, 34, 10, 2, 10, 19 };
                var dependents = new List<Dependent>();

                if (numberOfDepenedents > names.Length)
                {
                    numberOfDepenedents = names.Length;
                }

                for (var i = 0; i < numberOfDepenedents; i++)
                {
                    var newDep = new Dependent
                    {
                        Id = i + 1,
                        FirstName = names[i],
                        LastName = "Simpson",
                        Relationship = i == 0 ? Relationship.Spouse : Relationship.Spouse,
                        DateOfBirth = DateTime.Now.AddYears(ages[i] * -1)
                    };

                    dependents.Add(newDep);
                }

                return new Employee
                {
                    DateOfBirth = DateTime.Now.AddYears(age * -1),
                    FirstName = "Homer",
                    LastName = "Simpson",
                    Id = empId,
                    Salary = salary,
                    Dependents = dependents
                };
            }
        }
    }
}
