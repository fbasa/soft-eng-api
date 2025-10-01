namespace SoftEng.Domain;

public static class StudentIdGenerator
{
    private static int counter = 1; // You can persist this if needed

    public static string GenerateStudentId(string courseCode = "CS305", string campusCode = "SJC")
    {
        string uniquePart = counter.ToString("D4"); // Pads with leading zeros (e.g., 001, 002)
        counter++; // Increment for next ID
        return $"{courseCode}-{campusCode}-{uniquePart}";
    }
}