using System;

namespace SoftEng.Domain;

public static class StudentIdGenerator
{
    private static readonly Random _random = new();

    public static string GenerateStudentId(string courseCode = "CS305", string campusCode = "SJC")
    {
        string timestamp = DateTime.UtcNow.ToString("yyMMddHHmmssfff");

        return $"{courseCode}-{campusCode}-{timestamp}";
    }
}