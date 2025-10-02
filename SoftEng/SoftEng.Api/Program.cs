using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.OpenApi.Models;
using Serilog;
using SoftEng.Api.Errors;
using SoftEng.Application;
using SoftEng.Infrastructure;
using StackExchange.Redis;


var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();


var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));

// Dependency Injection
builder.Services.AddApplication(config);
builder.Services.AddInfrastructure(config);

builder.Services.AddControllers();

// Global exception handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>(); 

//CORS
builder.Services.AddCors(cors =>
{
    cors.AddPolicy("corspolicy", (builder) =>
    {
        builder.WithOrigins("http://localhost:4200", "https://fbasa.github.io")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials() //Allow SignalR endpoint to negotiate
        .WithExposedHeaders("Content-Disposition", "Content-Type");
    });
});

// Distributed caching
var redisConnectionString = config.GetConnectionString("Redis");

if (!string.IsNullOrWhiteSpace(redisConnectionString))
{
    builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    {
        var redisOptions = ConfigurationOptions.Parse(redisConnectionString, true);
        redisOptions.AbortOnConnectFail = false;
        redisOptions.ConnectRetry = 3;
        redisOptions.SyncTimeout = 5000;
        return ConnectionMultiplexer.Connect(redisOptions);
    });

    builder.Services.AddSingleton<IDistributedCache>(sp =>
        new RedisCache(new RedisCacheOptions
        {
            // Reuse the existing multiplexer instead of creating a new one
            ConnectionMultiplexerFactory = () => Task.FromResult(sp.GetRequiredService<IConnectionMultiplexer>()),
            InstanceName = "soft_eng:" // key prefix
        }));
}

//fallback to Memory cache
builder.Services.AddMemoryCache();

//Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SoftEng API", Version = "v1" });
});

var app = builder.Build();


app.UseSwagger();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    var provider = app.Services.GetRequiredService<Asp.Versioning.ApiExplorer.IApiVersionDescriptionProvider>();
    foreach (var desc in provider.ApiVersionDescriptions)
        c.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", $"SoftEng API {desc.GroupName}");
});

app.UseCors("corspolicy");
app.UseOutputCache();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();