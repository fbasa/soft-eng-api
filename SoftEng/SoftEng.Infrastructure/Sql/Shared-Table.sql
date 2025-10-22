USE [SoftEng]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TABLE SharedTable (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Type NVARCHAR(50) NOT NULL, -- e.g. "Gender", "Programs"
    Name NVARCHAR(100) NOT NULL, -- e.g. "Male", "Female", "BSCS"
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);

GO

INSERT INTO SharedTable (Type, Name) VALUES
('Gender', 'Male'),
('Gender', 'Female'),
('Gender', 'Kurapika');

GO