CREATE TABLE Courses(
	CourseID INT Identity(1,1) NOT NULL PRIMARY KEY,
	CourseName VARCHAR(100) NOT NULL,
	[CourseCode] NVARCHAR(20) NOT NULL UNIQUE,
    [Instructor] NVARCHAR(100) NOT NULL,
    [Capacity] INT NOT NULL,
    [Enrolled] INT NOT NULL DEFAULT 0,
    [Schedules] NVARCHAR(255) NOT NULL,
    [Description] NVARCHAR(500) NULL
);