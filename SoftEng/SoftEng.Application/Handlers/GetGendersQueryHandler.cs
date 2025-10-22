using MediatR;
using SoftEng.Application.Contracts;
using SoftEng.Domain.Request;
namespace SoftEng.Application.Handlers
{
    public record GetGendersQuery(GetGendersRequest Request) : IRequest<IReadOnlyList<string>>;


    public class GetGendersQueryHandler(ISharedRepository repo) : IRequestHandler<GetGendersQuery, IReadOnlyList<string>>
    {
        public async Task<IReadOnlyList<string>> Handle(GetGendersQuery request, CancellationToken ct)
        {
            var genders = await repo.GetGendersAsync(ct);
            return genders.Select(g => g.Name ).ToList();
        }
    }
}
