namespace SoftEng.Domain.Request;

public record GetStudentListRequest
{
    public int Page { get; init; } = 1;
    public int Size { get; init; } = 10;
};
