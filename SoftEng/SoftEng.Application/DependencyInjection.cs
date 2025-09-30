using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SoftEng.Application.Caching;
using SoftEng.Application.Common;

namespace SoftEng.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
    {
        //MediatR
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining<ICacheableQuery>())
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(QueryCacheBehavior<,>));


        //Auto Mapper
        services.AddSingleton(sp => new MapperConfiguration(config =>
        {
            config.AddProfile<AutoMapperProfile>();
        }, sp.GetRequiredService<ILoggerFactory>()).CreateMapper());


        // Api versioning
        VersionConfig.AddApiVersioning(services);

        //Output caching
        services.AddOutputCachingWithPolicies(config);

        return services;
    }
}
