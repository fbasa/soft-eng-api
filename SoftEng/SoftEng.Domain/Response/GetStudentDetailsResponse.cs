using SoftEng.Domain.Model;

namespace SoftEng.Domain.Response;

public class GetStudentDetailsResponse : GetStudentResult
{
}

public class GetStudentResult : StudentModel
{
    public int Id { get; set; }
    public string StudentId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
