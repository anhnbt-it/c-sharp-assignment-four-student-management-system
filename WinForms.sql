USE [master]

IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'C0501L_WinForm')
	DROP DATABASE [C0501L_WinForm]
GO

CREATE DATABASE [C0501L_WinForm]
GO

use C0501L_WinForm

SET NOCOUNT ON

if exists (select * from dbo.sysobjects where id = object_id(N'dbo.Language') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.Language
GO
CREATE TABLE dbo.Language
(
LanguageID                          INT IDENTITY(1,1) NOT NULL,
LanguageName                        NVARCHAR(30) NOT NULL,
CONSTRAINT PK_Language PRIMARY KEY CLUSTERED ( LanguageID )
)

GO
if exists (select * from dbo.sysobjects where id = object_id(N'dbo.Student') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.Student
GO
CREATE TABLE dbo.Student
(
StudentID                           INT IDENTITY(1,1) NOT NULL,
StudentName                         NVARCHAR(50),
Age                                 INT,
Email                               NVARCHAR(30),
Phone                               NVARCHAR(20),
ImagePath                           NVARCHAR(255),
CONSTRAINT PK_Student PRIMARY KEY CLUSTERED ( StudentID )
)

GO
if exists (select * from dbo.sysobjects where id = object_id(N'dbo.StudentLanguage') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.StudentLanguage
GO
CREATE TABLE dbo.StudentLanguage
(
LanguageID                          INT NOT NULL,
StudentID                           INT NOT NULL,
CONSTRAINT PK_StudentLanguage PRIMARY KEY CLUSTERED ( LanguageID,StudentID )
)

GO
ALTER TABLE dbo.Language NOCHECK CONSTRAINT ALL
GO

TRUNCATE TABLE dbo.Language
GO

IF (IDENT_SEED('dbo.Language') IS NOT NULL )	SET IDENTITY_INSERT dbo.Language ON
INSERT INTO dbo.Language (LanguageID,LanguageName) VALUES('1','English')
INSERT INTO dbo.Language (LanguageID,LanguageName) VALUES('2','French')
INSERT INTO dbo.Language (LanguageID,LanguageName) VALUES('3','Chinese')
INSERT INTO dbo.Language (LanguageID,LanguageName) VALUES('4','Japanese')
INSERT INTO dbo.Language (LanguageID,LanguageName) VALUES('5','Russian')
IF (IDENT_SEED('dbo.Language') IS NOT NULL )	SET IDENTITY_INSERT dbo.Language OFF
GO
GO
ALTER TABLE dbo.Language CHECK CONSTRAINT ALL
GO


ALTER TABLE dbo.Student NOCHECK CONSTRAINT ALL
GO

TRUNCATE TABLE dbo.Student
GO

IF (IDENT_SEED('dbo.Student') IS NOT NULL )	SET IDENTITY_INSERT dbo.Student ON
INSERT INTO dbo.Student (StudentID,StudentName,Age,Email,Phone,ImagePath) VALUES('1','Tran Van Ngan','20','tvngan@yahoo.com','4134124',NULL)
INSERT INTO dbo.Student (StudentID,StudentName,Age,Email,Phone,ImagePath) VALUES('2','Quan Thi Lien','19','qtlien@yahoo.com','7567234',NULL)
INSERT INTO dbo.Student (StudentID,StudentName,Age,Email,Phone,ImagePath) VALUES('3','Nguyen Trac Dat','21','ntdat@yahoo.com','4234566',NULL)
INSERT INTO dbo.Student (StudentID,StudentName,Age,Email,Phone,ImagePath) VALUES('4','Tran Quang Huy','21','tqhuy@yahoo.com','5345346',NULL)
IF (IDENT_SEED('dbo.Student') IS NOT NULL )	SET IDENTITY_INSERT dbo.Student OFF
GO
GO
ALTER TABLE dbo.Student CHECK CONSTRAINT ALL
GO


ALTER TABLE dbo.StudentLanguage NOCHECK CONSTRAINT ALL
GO

TRUNCATE TABLE dbo.StudentLanguage
GO

IF (IDENT_SEED('dbo.StudentLanguage') IS NOT NULL )	SET IDENTITY_INSERT dbo.StudentLanguage ON
INSERT INTO dbo.StudentLanguage (LanguageID,StudentID) VALUES('1','1')
INSERT INTO dbo.StudentLanguage (LanguageID,StudentID) VALUES('1','2')
INSERT INTO dbo.StudentLanguage (LanguageID,StudentID) VALUES('1','3')
INSERT INTO dbo.StudentLanguage (LanguageID,StudentID) VALUES('1','4')
INSERT INTO dbo.StudentLanguage (LanguageID,StudentID) VALUES('2','3')
INSERT INTO dbo.StudentLanguage (LanguageID,StudentID) VALUES('3','2')
IF (IDENT_SEED('dbo.StudentLanguage') IS NOT NULL )	SET IDENTITY_INSERT dbo.StudentLanguage OFF
GO
GO
ALTER TABLE dbo.StudentLanguage CHECK CONSTRAINT ALL
GO


