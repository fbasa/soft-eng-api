using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.OpenApi.Models;
using Serilog;
using SoftEng.Application.Caching;
using SoftEng.Application.Common;
using SoftEng.Infrastructure;
using SoftEng.Infrastructure.Concretes;
using SoftEng.Infrastructure.Contracts;
using StackExchange.Redis;


var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();


var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));

builder.Services.Configure<DapperOptions>(builder.Configuration.GetSection("Dapper"));

//Polly
builder.Services.AddSingleton(sp =>
    SqlRetryHelper.CreateTimeoutRetryPipeline(
        new SqlRetryHelper.SqlRetryOptions { MaxRetryAttempts = 3 }));

//Repositories
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
//TODO: Add more repository here


//MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<ICacheableQuery>())
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(QueryCacheBehavior<,>));



//Dapper
builder.Services.AddScoped<IDapperBaseService, DapperBaseService>();
builder.Services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();

//Auto Mapper
builder.Services.AddSingleton(sp => new MapperConfiguration(config =>
{
    config.AddProfile<AutoMapperProfile>();
},sp.GetRequiredService<ILoggerFactory>()).CreateMapper());

builder.Services.AddControllers();

// Api versioning
VersionConfig.AddApiVersioning(builder.Services);

//Output caching
builder.Services.AddOutputCachingWithPolicies(config);

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

//Distibuted caching
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var cfg = ConfigurationOptions.Parse(config.GetConnectionString("Redis")!, true);
    cfg.AbortOnConnectFail = false;
    cfg.ConnectRetry = 3;
    cfg.SyncTimeout = 5000;
    return ConnectionMultiplexer.Connect(cfg);
}).AddSingleton<IDistributedCache>(sp =>
{
    return new RedisCache(new RedisCacheOptions
    {
        // Reuse the existing multiplexer instead of creating a new one
        ConnectionMultiplexerFactory = () => Task.FromResult(sp.GetRequiredService<IConnectionMultiplexer>()),
        InstanceName = "soft_eng:" // key prefix
    });
});
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