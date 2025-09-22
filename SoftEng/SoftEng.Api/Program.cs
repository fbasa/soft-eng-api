using Serilog;
using SoftEng.Infrastructure;
using SoftEng.Infrastructure.Concretes;
using SoftEng.Infrastructure.Contracts;


var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));

builder.Services.Configure<DapperOptions>(builder.Configuration.GetSection("Dapper"));

builder.Services.AddSingleton(sp =>
    SqlRetryHelper.CreateTimeoutRetryPipeline(
        new SqlRetryHelper.SqlRetryOptions { MaxRetryAttempts = 3 }));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IDapperBaseService, DapperBaseService>();
builder.Services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "SoftEng API",
        Version = "v1"
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "SoftEng API");
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();