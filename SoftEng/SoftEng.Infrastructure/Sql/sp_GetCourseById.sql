CREATE PROCEDURE sp_GetCourseById
	@CourseID INT
AS
BEGIN
	SET NOCOUNT ON;

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
	WHERE CourseID = @CourseID;
END