using System;
namespace DigitalBank.Web.Data;

public class LogApplication
{
    public string Id { get; set; }
    public DateTime Logged { get; set; }
    public string LogLevel { get; set; }
    public string Message { get; set; }
    public string Exception { get; set; }
    public string TraceException { get; set; }
}

