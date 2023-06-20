namespace DigitalBank.Web.Services
{
    public interface ILogService
    {
        ValueTask<LogApplication> CreateLogAsync(LogApplication logApplication);
    }
}