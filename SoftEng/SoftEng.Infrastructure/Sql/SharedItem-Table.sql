USE [SoftEng]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TABLE SharedItem (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Type NVARCHAR(50) NOT NULL, -- e.g. "Gender", "Programs"
    Name NVARCHAR(100) NOT NULL, -- e.g. "Male", "Female", "BSCS"
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);

GO



INSERT INTO SharedItem (Type, Name)
SELECT 'Gender', 'Male'
WHERE NOT EXISTS (SELECT 1 FROM SharedItem WHERE Type = 'Gender' AND Name = 'Male');

INSERT INTO SharedItem (Type, Name)
SELECT 'Gender', 'Female'
WHERE NOT EXISTS (SELECT 1 FROM SharedItem WHERE Type = 'Gender' AND Name = 'Female');
GO

INSERT INTO SharedItem (Type, Name)
SELECT 'Semester', '1st'
WHERE NOT EXISTS (SELECT 1 FROM SharedItem WHERE Type = 'Semester' AND Name = '1st');

INSERT INTO SharedItem (Type, Name)
SELECT 'Semester', '2nd'
WHERE NOT EXISTS (SELECT 1 FROM SharedItem WHERE Type = 'Semester' AND Name = '2ndKinley');

INSERT INTO SharedItem (Type, Name)
SELECT 'Program', 'BSCS'
WHERE NOT EXISTS (SELECT 1 FROM SharedItem WHERE Type = 'Program' AND Name = 'BSCS');

INSERT INTO SharedItem (Type, Name)
SELECT 'Program', 'BSED'
WHERE NOT EXISTS (SELECT 1 FROM SharedItem WHERE Type = 'Program' AND Name = 'BSED');
