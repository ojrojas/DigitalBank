using DigitalBank.Api.DI;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
var connection = configuration["ConnectionString"];
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<OptionToken>(configuration.GetSection("OptionToken"));
builder.Services.Configure<ElasticsearchConfiguration>(configuration.GetSection("ElasticSettings"));

builder.Services.AddSingleton(new RedisConnectionProvider(builder.Configuration["ConnectionString"]));

builder.Services.AddHostedService<RedisService>();

builder.Services.AddJwtExtension(configuration);
builder.Services.AddSwaggerGenDocumention();

builder.WebHost.ConfigureKestrel(options =>
{
    // Setup a HTTP/2 endpoint without TLS.
    options.Listen(System.Net.IPAddress.Any, configuration.GetValue("API_PORT", 5083), listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
