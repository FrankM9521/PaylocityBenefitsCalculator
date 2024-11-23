using Api.BusinessLogic.Models;

namespace Api.Api.Dtos.Dependent;

/* if I had more time I would make these records*/
public class GetDependentDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Relationship Relationship { get; set; }
    public int EmployeeId { get; set; }
}
