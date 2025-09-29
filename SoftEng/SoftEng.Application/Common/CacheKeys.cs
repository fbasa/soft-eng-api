using System.Collections.Concurrent;

namespace SoftEng.Application.Common;

public static class CacheKeys
{
    private static readonly ConcurrentBag<string> Keys = new();

    public static void Add(string key) => Keys.Add(key);

    public static IEnumerable<string> GetKeys(string prefix) =>
        Keys.Where(k => k.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
}
