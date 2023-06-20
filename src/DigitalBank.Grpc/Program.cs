using DigitalBank.Grpc.Services;
using DigitalBank.Infraestructure.Repositories;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

builder.Services.AddTransient(typeof(GenericRepository<>));

builder.Services.AddTransient(typeof(PersonRepository));

builder.WebHost.ConfigureKestrel(options =>
{
    // Setup a HTTP/2 endpoint without TLS.
    options.Listen(System.Net.IPAddress.Any, configuration.GetValue("GRPC_PORT", 5222), listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});


// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
var app = builder.Build();



// Configure the HTTP request pipeline.
app.MapGrpcService<PersonService>();

IWebHostEnvironment env = app.Environment;
if (env.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
