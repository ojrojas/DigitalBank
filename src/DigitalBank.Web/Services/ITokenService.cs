namespace DigitalBank.Web.Services;

public interface ITokenService
{
    ValueTask<string> GetTokenAsync();
}