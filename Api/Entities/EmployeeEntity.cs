using Api.Entities;

namespace Api.Enitiies;

//Normally I would not post-fix with "entity",
// I would not use an entity outside of my repository layer. 
//
//and if forced to, would use an annotation to rename the table
//[Table("Employee")]
public class EmployeeEntity : EntityBase
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<DependentEntity> Dependents { get; set; } = new List<DependentEntity>();
}
