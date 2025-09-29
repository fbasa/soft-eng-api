using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SoftEng.Application.Common;
using System.Text.Json;

namespace SoftEng.Application.Caching;

public sealed class QueryCacheBehavior<TRequest, TResponse>(IMemoryCache mem, ILoggerFactory factory, IDistributedCache? dist = null)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        var logger = factory.CreateLogger("QueryCacheBehavior");

        if (request is not ICacheableQuery cq) return await next();

        if (dist is null)
        {
            return await FallbackAsync(next, cq);
        }

        try
        {
            var cached = await dist.GetStringAsync(cq.CacheKey, ct);
            if (cached is not null) return JsonSerializer.Deserialize<TResponse>(cached)!;

            var resp = await next();
            var ttl = cq.Ttl ?? TimeSpan.FromSeconds(30);
            await dist.SetStringAsync(
                cq.CacheKey,
                JsonSerializer.Serialize(resp),
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = ttl },
                ct);
            CacheKeys.Add(cq.CacheKey);
            return resp;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Distributed cache unavailable. Falling back to memory cache.");
            return await FallbackAsync(next, cq);
        }
    }

    private async Task<TResponse> FallbackAsync(RequestHandlerDelegate<TResponse> next, ICacheableQuery cq)
    {
        if (mem.TryGetValue(cq.CacheKey, out TResponse hit)) return hit!;

        var resp = await next();
        mem.Set(cq.CacheKey, resp, cq.Ttl ?? TimeSpan.FromSeconds(30));
        CacheKeys.Add(cq.CacheKey);
        return resp;
    }
}