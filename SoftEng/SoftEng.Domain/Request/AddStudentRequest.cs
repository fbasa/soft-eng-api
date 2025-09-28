namespace SoftEng.Domain.Request;

public class AddStudentRequest
{
    public required string FirstName { get; set; }
    public int Age { get; set; }
    public int Gender { get; set; }
}
