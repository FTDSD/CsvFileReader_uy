
CREATE TABLE [dbo].[Student](
	[Id] [int] primary key,
	[Name] [varchar](100) NULL,
	
) 

create table [dbo].[StudentDetail]
(
  DetailId int identity(1,1),
  Id int FOREIGN KEY (Id) REFERENCES Student(Id),
  [Marks] [int] NULL,
	[Subject] [varchar](100) NULL,
	[ExamDate] [date] NULL
)