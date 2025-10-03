using SoftEng.Domain.Request;

namespace SoftEng.Domain.Response;

public class GetStudentListResponse 
{
    public IReadOnlyList<GetStudentResult>? Items { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}

public class GetStudentListResult : StudentModel
{
    public int Id { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public string StudentId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}

public class GetStudentResult : StudentModel
{
    public int Id { get; set; }
    public string StudentId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}