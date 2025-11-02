CREATE PROCEDURE sp_GetCourses
    @Page INT = 1,
    @Size INT = 20,
    @TotalCount INT OUTPUT,
    @TotalPages INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Offset INT = (@Page - 1) * @Size;

    -- Get total count and pages
    SELECT @TotalCount = COUNT(*) FROM Courses;
    SELECT @TotalPages = CEILING(1.0 * @TotalCount / @Size);

    -- Get paginated course data
    SELECT 
        CourseID,
        CourseName,
        CourseCode,
        Instructor,
        Capacity,
        Enrolled,
        Schedules,
        Description
    FROM Courses
    ORDER BY CourseID
    OFFSET @Offset ROWS
    FETCH NEXT @Size ROWS ONLY;
END