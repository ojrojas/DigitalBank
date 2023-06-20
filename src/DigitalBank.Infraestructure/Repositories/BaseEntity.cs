namespace DigitalBank.Infraestructure.Repositories;

/// <summary>
/// Base entity
/// </summary>
public class BaseEntity
{
	[Key]
	public Guid Id { get; set; }
	public DateTime CreatedOn { get; set; }
	public DateTime UpdatedOn { get; set; }
}