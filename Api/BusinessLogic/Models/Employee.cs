﻿namespace Api.BusinessLogic.Models;

public class Employee
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public IEnumerable<Dependent> Dependents { get; set; } = new List<Dependent>();
}