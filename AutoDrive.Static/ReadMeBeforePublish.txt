add in jobName Table مهندس becouse it is in Enum
add in jobfunction Table تقييم becouse it is in Enum

USE [AutoDrive]
GO
SET IDENTITY_INSERT [dbo].[JobName] ON
INSERT INTO [dbo].[JobName]
           ([ID],[Name]
           ,[EnName])
     VALUES
           (33,N'مهندس','Engineer')

SET IDENTITY_INSERT [dbo].[JobName] OFF 
GO


USE [AutoDrive]
GO
SET IDENTITY_INSERT [dbo].[JobFunction] ON
		   INSERT INTO [dbo].[JobFunction]
           ([ID],[Name]
		   ,[JobNameId],[EnName])
     VALUES
           (5,N'تقييم'
		   ,'33','Evaluation')
SET IDENTITY_INSERT [dbo].[JobFunction] OFF 
GO


USE [AutoDrive]
GO
SET IDENTITY_INSERT [dbo].[JobName] ON
INSERT INTO [dbo].[JobName]
           ([ID],[Name]
           ,[EnName])
     VALUES
           (36,N'امين خزنه','Treasurer')

SET IDENTITY_INSERT [dbo].[JobName] OFF 
GO


USE [AutoDrive]
GO
SET IDENTITY_INSERT [dbo].[EmplpoyeeMoneyType] ON

INSERT INTO [dbo].[EmplpoyeeMoneyType]
           ([ID],[Name]
           ,[EnName])
     VALUES
           (2,'مكافاة','Reward')
INSERT INTO [dbo].[EmplpoyeeMoneyType]
           ([ID],[Name]
           ,[EnName])
     VALUES
           (1,N'راتب','Salary')


SET IDENTITY_INSERT [dbo].[EmplpoyeeMoneyType] OFF 

USE [AutoDrive]
GO
SET IDENTITY_INSERT [dbo].[EmployeeMoneyTypeDetails] ON
INSERT INTO [dbo].[EmployeeMoneyTypeDetails]
           ([ID],[Name]
           ,[EnName])
     VALUES
           (3,N'مكافاة','Reward')
SET IDENTITY_INSERT [dbo].[EmployeeMoneyTypeDetails] OFF 
GO

USE [AutoDrive]
GO
SET IDENTITY_INSERT [dbo].[EmployeeStatus] ON
INSERT INTO [dbo].[EmployeeStatus]
           ([ID],[Name]
           ,[EnName])
     VALUES
          (1,N'بالخدمه','inservice'),
		  (3,N'إنهاء خدمة','EndOfService')
 SET IDENTITY_INSERT [dbo].[EmployeeStatus] OFF 
GO

USE [AutoDrive]
GO
SET IDENTITY_INSERT [dbo].[EmployeeStatusKind] ON
INSERT INTO [dbo].[EmployeeStatusKind]
           ([ID],[Name]
           ,[EnName]
           ,[EmployeeStatusId])
     VALUES
           (1,N'تحت التجربه','Under Experience',1),
		   (3,N'مثبت','Fixed',1)
 SET IDENTITY_INSERT [dbo].[EmployeeStatusKind] OFF 
GO


USE [AutoDrive]
GO
SET IDENTITY_INSERT [dbo].[EmployeeMoneyTypeDetails] ON

INSERT INTO [dbo].[EmployeeMoneyTypeDetails]
           ([ID],[Name]
           ,[EnName])
     VALUES
           (4,N'جزاء','Violation'),
		   (6,N'اساسى','Basic'),
		   (7,N'متغير','Changed')
SET IDENTITY_INSERT [dbo].[EmployeeMoneyTypeDetails] OFF 
GO

USE [AutoDrive]
GO
SET IDENTITY_INSERT [dbo].[EmployeeMoneyTypeDetails] ON
INSERT INTO [dbo].[EmployeeMoneyTypeDetails]
           ([ID],[Name]
           ,[EnName])
     VALUES
           (8,N'قرض او سلفة','Loan'),
		   (9,N'خصم اجازة','Vacation Deduction')
		   
SET IDENTITY_INSERT [dbo].[EmployeeMoneyTypeDetails] OFF 
GO



USE [AutoDrive]
GO
SET IDENTITY_INSERT [dbo].[EmployeeStatus] ON

INSERT INTO [dbo].[EmployeeStatus]
           ([ID],[Name]
           ,[EnName])
     VALUES
           (1,'بالخدمة','In Service'),
		   (3,'إنهاء الخدمة','End Of Service')
SET IDENTITY_INSERT [dbo].[EmployeeStatus] OFF 
GO