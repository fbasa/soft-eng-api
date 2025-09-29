using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using SoftEng.Application.Common;

namespace SoftEng.Application.Caching.Invalidator;

public sealed record StudentListChanged() : INotification;

public sealed class StudentListCacheInvalidator(IMemoryCache memory, IDistributedCache? dist = null) : INotificationHandler<StudentListChanged>
{
    public async Task Handle(StudentListChanged n, CancellationToken ct)
    {
        foreach (var key in CacheKeys.GetKeys(OutputCachedKeyNames.StudentList))
        {
            memory.Remove(key);
            if (dist is not null)
                await dist.RemoveAsync(key, ct);
        }
    }
}
