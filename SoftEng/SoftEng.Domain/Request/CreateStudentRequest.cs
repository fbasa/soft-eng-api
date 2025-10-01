namespace SoftEng.Domain.Request;

public class StudentModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string EmailAddress { get; set; }
    public required string PhoneNumber { get; set; }
    public DateTime DOB { get; set; }
    public required string Gender { get; set; }
    public  string? SchoolYear { get; set; }
    public  string? YearSemester { get; set; }
    public  string? ProgramClass { get; set; }
    public  string? HomeAddress { get; set; }
    public  string? EmergencyContact { get; set; }
    public  string? EmergencyPhone { get; set; }
    public  string? AdditonalNotes { get; set; }

}
public class CreateStudentRequest : StudentModel
{
    public string StudentId { get; set; } = Guid.NewGuid().ToString();
}
