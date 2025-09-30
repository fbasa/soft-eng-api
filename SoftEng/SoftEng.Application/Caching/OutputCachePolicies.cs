using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SoftEng.Application.Caching;

public static class OutputCachePolicies
{
    public static IServiceCollection AddOutputCachingWithPolicies(this IServiceCollection services, IConfiguration cfg)
    {
        var listTtl = cfg.GetSeconds("Caching:ListTtlSeconds", 30);

        services.AddOutputCache(o =>
        {
            o.AddPolicy(OutputCachePolicyNames.List30s, b => b.Expire(TimeSpan.FromSeconds(listTtl)));

            //TODO: Add more policies
        });
        return services;
    }

    private static int GetSeconds(this IConfiguration cfg, string key, int fallback) => int.TryParse(cfg[key], out var v) && v > 0 ? v : fallback;
  
}
