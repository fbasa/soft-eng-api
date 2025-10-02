using SoftEng.Domain.Request;

namespace SoftEng.Domain.Response;

public class GetStudentListResponse : CreateStudentRequest
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }

}
