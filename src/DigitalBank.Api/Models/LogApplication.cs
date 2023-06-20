using Redis.OM.Modeling;

namespace DigitalBank.Api.Models;

[Document(StorageType = StorageType.Json, Prefixes = new[] { "LogApplication" })]
public class LogApplication
{
    [RedisIdField][Indexed] public string Id { get; set; }
    [Indexed] public DateTime Logged { get; set; }
    [Indexed] public string LogLevel { get; set; }
    [Indexed] public string Message { get; set; }
    [Indexed] public string Exception { get; set; }
    [Indexed] public string TraceException { get; set; }
}

