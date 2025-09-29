using Azure.Core;
using SoftEng.Domain.Request;
using SoftEng.Domain.Response;
using SoftEng.Infrastructure.Contracts;
using System.Data;

namespace SoftEng.Infrastructure.Concretes;

public class StudentRepository(IDapperBaseService dapper) : IStudentRepository
{
    public async Task<int> AddStudentAsync(AddStudentRequest request, CancellationToken ct)
    {
        var parameters = RequestParameterBuilder<AddStudentRequest>
                            .For(request)
                            .Input(i => i.FirstName)
                            .Input(i => i.Age)
                            .Input(i => i.Gender)
                            .Output("StudentId", DbType.Int32)
                            .Build();

        await dapper.ExecuteCommandAsync("sp_InsertStudent", parameters, ct);

        return parameters.Get<int>("StudentId");
    }

    public async Task<GetStudentDetailsResponse> GetStudentDetailsAsync(GetStudentDetailsRequest request, CancellationToken ct)
    {
        var parameters = RequestParameterBuilder<GetStudentDetailsRequest>
                    .For(request)
                    .Input("StudentId",x => x.Id)
                    .Build();
        var result = await dapper.SqlQueryAsync<GetStudentDetailsResponse>("sp_GetStudentById", parameters, ct);
        return result.FirstOrDefault()!;
    }

    public async Task<IReadOnlyList<GetStudentListResponse>> GetStudentsAsync(CancellationToken ct)
    {
        var result = await dapper.SqlQueryAsync<GetStudentListResponse>("sp_GetStudents", null, ct);
        return result.ToList();
    }
}
