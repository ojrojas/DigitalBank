namespace DigitalBank.Api.Models;

public class ApplicationIdentity
{
	public Guid Id { get; set; }
	public string ApplicationName { get; set; } = null!;
	public string PassAccess { get; set; } = null!;
}

