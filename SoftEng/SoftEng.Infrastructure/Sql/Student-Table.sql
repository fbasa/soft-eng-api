USE [SoftEng]
GO

/****** Object:  Table [dbo].[Student]    Script Date: 10/17/2025 3:45:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Student](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NULL,
	[StudentId] [varchar](50) NULL,
	[EmailAddress] [varchar](50) NULL,
	[PhoneNumber] [varchar](12) NULL,
	[DOB] [date] NULL,
	[Gender] [varchar](10) NULL,
	[SchoolYear] [varchar](20) NULL,
	[YearSemester] [varchar](20) NULL,
	[ProgramClass] [varchar](50) NULL,
	[HomeAddress] [varchar](200) NULL,
	[EmergencyContact] [varchar](50) NULL,
	[EmergencyPhone] [varchar](12) NULL,
	[AdditonalNotes] [varchar](200) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Student] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO


