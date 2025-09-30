using Azure.Core;
using System.Data;
using SoftEng.Domain.Request;
using SoftEng.Domain.Response;
using SoftEng.Application.Contracts;

namespace SoftEng.Infrastructure.Concretes;

public class StudentRepository(IDapperBaseService dapper) : IStudentRepository
{
    public async Task<int> AddStudentAsync(CreateStudentRequest request, CancellationToken ct)
    {
        var parameters = RequestParameterBuilder<CreateStudentRequest>
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

    public async Task<IReadOnlyList<GetStudentListResponse>> GetStudentsAsync(GetStudentListRequest request, CancellationToken ct)
    {
        var parameters = RequestParameterBuilder<GetStudentListRequest>
                  .For(request)
                  .Input(x => x.Page)
                  .Input(x => x.Size)
                  .Build();
        var result = await dapper.SqlQueryAsync<GetStudentListResponse>("sp_GetStudents", parameters, ct);
        return result.ToList();
    }

    public async Task<int> UpdateStudentAsync(UpdateStudentRequest request, CancellationToken ct)
    {
        var parameters = RequestParameterBuilder<UpdateStudentRequest>
                            .For(request)
                            .Input(i => i.Id)
                            .Input(i => i.FirstName)
                            .Input(i => i.Age)
                            .Input(i => i.Gender)
                            .Build();

        return await dapper.ExecuteCommandAsync("sp_UpdateStudent", parameters, ct);
    }
}
