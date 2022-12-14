--USE [uygoman]
GO
/****** Object:  StoredProcedure [dbo].[spUpdateStudentDetails]    Script Date: 8/18/2022 5:45:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
 
create PROCEDURE [dbo].[spUpdateStudentDetails]
	@Action varchar(20)=null,
	@StudentId int=null,
	@Name varchar(200) = null,
	@Marks int = null,
	@Subject varchar(100) = null,
	@ExamDate datetime = null
AS
SET NOCOUNT OFF;

IF @Action ='IsStudentIdAvailable'
BEGIN
	select COUNT(Name) as count from dbo.Student where Id=@StudentId
END

IF @Action ='InsertStudent'
BEGIN
	Insert into dbo.Student(Id,Name) values(@StudentId,@Name)
END

IF @Action ='InsertStudentDetails'
BEGIN
	Insert into dbo.StudentDetail(Id,Marks,Subject,ExamDate) values(@StudentId,@Marks,@Subject,@ExamDate)
END

