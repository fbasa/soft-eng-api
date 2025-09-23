using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace SoftEng.Application.Caching;

public sealed class QueryCacheBehavior<TRequest, TResponse>(IDistributedCache dist, IMemoryCache mem, ILoggerFactory factory) 
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        var logger = factory.CreateLogger("QueryCacheBehavior");

        if (request is not ICacheableQuery cq) return await next();

        if(dist is not null)
        {
            try
            {
                var cached = await dist.GetStringAsync(cq.CacheKey, ct);
                if (cached is not null) return JsonSerializer.Deserialize<TResponse>(cached)!;

                var resp = await next();
                var ttl = cq.Ttl ?? TimeSpan.FromSeconds(30);
                await dist.SetStringAsync(cq.CacheKey, JsonSerializer.Serialize(resp),
                    new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = cq.Ttl }, ct);
                return resp;
            }
            catch (Exception ex)
            {
                //If Redis connection string not configured
                logger.LogError(ex.Message);

                return await fallback(next, cq);
            }
        }
        else
        {
            return await fallback(next, cq);
        }
    }

    private async Task<TResponse> fallback(RequestHandlerDelegate<TResponse> next, ICacheableQuery cq)
    {
        // fallback
        if (mem.TryGetValue(cq.CacheKey, out TResponse hit)) return hit!;
        var resp = await next();
        mem.Set(cq.CacheKey, resp, cq.Ttl ?? TimeSpan.FromSeconds(30));
        return resp;
    }
}