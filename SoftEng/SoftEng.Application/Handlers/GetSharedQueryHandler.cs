using MediatR;
using SoftEng.Application.Contracts;
using SoftEng.Domain.Request;
namespace SoftEng.Application.Handlers
{
    public record GetSharedQuery(GetSharedRequest Request) : IRequest<IReadOnlyList<string>>;


    public class GetSharedQueryHandler(ISharedRepository repo) : IRequestHandler<GetSharedQuery, IReadOnlyList<string>>
    {
        public async Task<IReadOnlyList<string>> Handle(GetSharedQuery query, CancellationToken ct)
        {
            var items = await repo.GetSharedItemsAsync(query.Request, ct);
            return items.Select(g => g.Name ).ToList();
        }
    }
}
