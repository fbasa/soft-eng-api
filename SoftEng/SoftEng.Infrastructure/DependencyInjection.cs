using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoftEng.Application.Contracts;
using SoftEng.Infrastructure.Repositories;

namespace SoftEng.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<DapperOptions>(config.GetSection("Dapper"));

        //Polly
        services.AddSingleton(sp =>
            SqlRetryHelper.CreateTimeoutRetryPipeline(
                new SqlRetryHelper.SqlRetryOptions { MaxRetryAttempts = 3 }));

        //Repositories
        services.AddScoped<IStudentRepository, StudentRepository>();
        //TODO: Add more repository here

        //Dapper
        services.AddScoped<IDapperBaseService, DapperBaseService>();
        services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();


        return services;
    }
}


