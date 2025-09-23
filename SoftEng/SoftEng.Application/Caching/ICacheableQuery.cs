﻿namespace SoftEng.Application.Caching;

public interface ICacheableQuery
{
    string CacheKey { get; }
    TimeSpan? Ttl { get; }
}
