using Paylocity.Shared.Entities;

namespace Paylocity.Employees.Entities;

//Normally I would not post-fix with "entity",
// I would not use an entity outside of my repository layer. 
//
//and if forced to, would use an annotation to rename the table
//[Table("Dependent")]
//
// Removed Dependent Property. It would cause a full graph to be created where each Dependent would lload a full employee
public class DependentEntity : EntityBase
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public RelationshipType Relationship { get; set; }
    public int EmployeeId { get; set; }
}
