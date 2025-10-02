using System;

namespace SoftEng.Domain;

public static class StudentIdGenerator
{
    private static int counter = 1; // You can persist this if needed
    private static readonly Random _random = new();

    public static string GenerateStudentId(string courseCode = "CS305", string campusCode = "SJC")
    {
        int randomNumber = _random.Next(10000, 100000); // 5-digit number, to be optimized later

        return $"{courseCode}-{campusCode}-{randomNumber}";
    }
}