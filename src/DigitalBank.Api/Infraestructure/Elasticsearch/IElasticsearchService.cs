namespace DigitalBank.Api.Infraestructure;

public interface IElasticsearchService
{
    ValueTask<LogApplication> CreateAsync(LogApplication entity, CancellationToken cancellationToken);
}