using SoftEng.Application.Contracts;
using SoftEng.Domain;
using SoftEng.Domain.Request;
using SoftEng.Domain.Response;
using SoftEng.Infrastructure.Dapper;
using System.Data;

namespace SoftEng.Infrastructure.Repositories;

internal sealed class StudentRepository(IDapperBaseService dapper) : IStudentRepository
{
    public async Task<int> CreateStudentAsync(CreateStudentRequest request, CancellationToken ct)
    {
        var parameters = RequestParameterBuilder<CreateStudentRequest>
                            .For(request)
                            .Input(i => i.FirstName)
                            .Input(i => i.LastName)
                            .Input("StudentId", i => StudentIdGenerator.GenerateStudentId())
                            .Input(i => i.EmailAddress)
                            .Input(i => i.PhoneNumber)
                            .Input(i => i.DOB)
                            .Input(i => i.Gender)
                            .Input(i => i.SchoolYear)
                            .Input(i => i.YearSemester)
                            .Input(i => i.ProgramClass)
                            .Input(i => i.HomeAddress)
                            .Input(i => i.EmergencyContact)
                            .Input(i => i.EmergencyPhone)
                            .Input(i => i.AdditonalNotes)
                            .Output("Id", DbType.Int32)
                            .Build();

        await dapper.ExecuteCommandAsync("sp_InsertStudent", parameters, ct);

        return parameters.Get<int>("Id");
    }

    public async Task<bool> DeleteStudentAsync(DeleteStudentRequest request, CancellationToken ct)
    {
        var parameters = RequestParameterBuilder<DeleteStudentRequest>
                   .For(request)
                   .Input("Id", x => x.Id)
                   .Build();
        var result = await dapper.ExecuteCommandAsync("sp_DeleteStudentById", parameters, ct);
        return true; //TODO: Change to check if row was actually deleted
    }

    public async Task<GetStudentDetailsResponse> GetStudentDetailsAsync(GetStudentDetailsRequest request, CancellationToken ct)
    {
        var parameters = RequestParameterBuilder<GetStudentDetailsRequest>
                    .For(request)
                    .Input("Id",x => x.Id)
                    .Build();
        var result = await dapper.SqlQueryAsync<GetStudentDetailsResponse>("sp_GetStudentById", parameters, ct);
        return result.FirstOrDefault()!;
    }

    public async Task<IReadOnlyList<GetStudentListResult>> GetStudentsAsync(GetStudentListRequest request, CancellationToken ct)
    {
        var parameters = RequestParameterBuilder<GetStudentListRequest>
                  .For(request)
                  .Input(x => x.Page)
                  .Input(x => x.Size)
                  .Build();
        var result = await dapper.SqlQueryAsync<GetStudentListResult>("sp_GetStudents", parameters, ct);
        return result.ToList();
    }

    public async Task<int> UpdateStudentAsync(UpdateStudentRequest request, CancellationToken ct)
    {
        var parameters = RequestParameterBuilder<UpdateStudentRequest>
                            .For(request)
                            .Input(i => i.Id)
                            .Input(i => i.FirstName)
                            .Input(i => i.LastName)
                            .Input(i => i.EmailAddress)
                            .Input(i => i.PhoneNumber)
                            .Input(i => i.DOB)
                            .Input(i => i.Gender)
                            .Input(i => i.SchoolYear)
                            .Input(i => i.YearSemester)
                            .Input(i => i.ProgramClass)
                            .Input(i => i.HomeAddress)
                            .Input(i => i.EmergencyContact)
                            .Input(i => i.EmergencyPhone)
                            .Input(i => i.AdditonalNotes)
                            .Build();

        return await dapper.ExecuteCommandAsync("sp_UpdateStudent", parameters, ct);
    }
    public Task<IReadOnlyList<string>> GetSchoolsAsync(CancellationToken ct)
    {
        var schools = new List<string>
        {
            "Computer Science ni Kinley",
            "Engineering",
            "Business",
            "Arts & Humanities",
            "Natural Sciences"
        };
        return Task.FromResult((IReadOnlyList<string>)schools);
    }
}
