using MediatR;
using SoftEng.Application.Contracts;
using SoftEng.Domain.Request;
using SoftEng.Domain.Response;
namespace SoftEng.Application.Handlers
{
    public record GetSharedQuery(GetSharedRequest Request) : IRequest<IReadOnlyList<SharedItemsResponse>>;


    public class GetSharedQueryHandler(ISharedRepository repo) : IRequestHandler<GetSharedQuery, IReadOnlyList<SharedItemsResponse>>
    {
        public async Task<IReadOnlyList<SharedItemsResponse>> Handle(GetSharedQuery query, CancellationToken ct)
        {
            return await repo.GetSharedItemsAsync(query.Request, ct);
        }
    }
}
