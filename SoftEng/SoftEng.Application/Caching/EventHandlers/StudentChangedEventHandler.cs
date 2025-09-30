using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using SoftEng.Application.Common;

namespace SoftEng.Application.Caching.EventHandlers;

public sealed record StudentChangedEvent() : INotification;

public sealed class StudentChangedEventHandler(IMemoryCache memory, IDistributedCache? dist = null) : INotificationHandler<StudentChangedEvent>
{
    public async Task Handle(StudentChangedEvent e, CancellationToken ct)
    {
        foreach (var key in CacheKeys.GetKeys(OutputCachedKeyNames.StudentList))
        {
            memory.Remove(key);
            if (dist is not null)
                await dist.RemoveAsync(key, ct);
        }
    }
}
