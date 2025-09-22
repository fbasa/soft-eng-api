namespace SoftEng.Domain.Response;

public class StudentResponse
{
    public DateOnly Date { get; set; }

    public int Gender { get; set; }

    public int Age { get; set; }

    public string? FirstName { get; set; }
}
