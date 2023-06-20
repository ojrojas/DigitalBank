namespace DigitalBank.Core.Entities;

/// <summary>
/// Person entity
/// </summary>
public class Person: BaseEntity , IAggregateRoot
{
    [MaxLength(100, ErrorMessage = "The property name can not be great that 100 characters")]
    public string Name { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    [MaxLength(1, ErrorMessage = "The property typesex can not be great that 1 character")]
    public string TypeSex { get; set; } = null!;
}

