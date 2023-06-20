namespace DigitalBank.Web.Data;

/// <summary>
/// Person 
/// </summary>
public class Person
{
    public Guid Id { get; set; }
    [StringLength(100, MinimumLength = 3)]
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public DateTime BirthDate { get; set; }
    [Required]
    public string TypeSex { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}

