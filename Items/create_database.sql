-- Database: DigitalBankDb

-- DROP DATABASE IF EXISTS "DigitalBankDb";

CREATE DATABASE DigitalBankDb;
Go
Use DigitalBankDb;
Go
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[BirthDate] [datetime] NOT NULL,
	[TypeSex] [nvarchar](1) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Person] ADD PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

Insert into Person(Name, BirthDate, TypeSex, CreatedOn) 
values 
('Oscar', '2023-06-13', '1', '2026-06-13'),
('Miguel', '2023-07-14', '1', '2026-06-13'),
('Sandra', '2023-08-13', '0', '2026-06-13'),
('Otoniel', '2023-06-13', '1', '2026-06-13'),
('Andrea', '2023-06-13', '0', '2026-06-13'),
('Andres', '2023-06-13', '1', '2026-06-13'),
('Maritza', '2023-06-13', '0', '2026-06-13'),
('Fernando', '2023-06-13', '1', '2026-06-13'),
('Teresa', '2023-06-13', '0', '2026-06-13'),
('Monica', '2023-06-13', '0', '2026-06-13'),
('Isabel', '2023-06-13', '0', '2026-06-13'),
('Patricia', '2023-06-13', '0', '2026-06-13'),
('Casandra', '2023-06-13', '0', '2026-06-13'),
('Lida', '2023-06-13', '0', '2026-06-13'),
('Marcela', '2023-06-13', '0', '2026-06-13'),
('Esperanza', '2023-06-13', '0', '2026-06-13'),
('Constanza', '2023-06-13', '0', '2026-06-13'),
('Vilma', '2023-06-13', '0', '2026-06-13'),
('Paola', '2023-06-13', '0', '2026-06-13'),
('Jenifer', '2023-06-13', '0', '2026-06-13'),
('Marge', '2023-06-13', '0', '2026-06-13'),
('Lisa', '2023-06-13', '0', '2026-06-13'),
('Eugenis', '2023-06-13', '0', '2026-06-13'),
('Eleanor', '2023-06-13', '0', '2026-06-13'),
('Godines', '2023-06-13', '1', '2026-06-13'),
('Moe', '2023-06-13', '1', '2026-06-13'),
('Carl', '2023-06-13', '1', '2026-06-13'),
('Lenny', '2023-06-13', '1', '2026-06-13'),
('Gorgori', '2023-06-13', '1', '2026-06-13'),
('Nelson', '2023-06-13', '1', '2026-06-13'),
('Patiño', '2023-06-13', '1', '2026-06-13'),
('Bob', '2023-06-13', '1', '2026-06-13'),
('Krusti', '2023-06-13', '1', '2026-06-13');

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_CreatePerson]
    @Id uniqueidentifier,
	@Name nvarchar(100),
	@BirthDate datetime, 
	@TypeSex nvarchar(1),
	@CreatedOn datetime
as
	insert into Person(id, name, birthdate, typesex, createdon)
	values (@Id, @Name, @BirthDate, @TypeSex, @CreatedOn)
	select id, name, birthdate, typesex, createdon from Person where id = @Id
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_DeletePerson]
    @Id uniqueidentifier
as
    select * into #temp from Person where id=@Id;
	delete from Person
	where id = @Id
    select * from #temp;
    drop table #temp;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_GetAllPersons]
   
as
	select id, name, birthdate, typesex, createdon from Person
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[SP_GetByIdPerson]
    @Id uniqueidentifier
as
	select id, name, birthdate, typesex, createdon, updatedon from Person
	where id = @Id
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[SP_UpdatePerson]
    @Id uniqueidentifier,
	@Name nvarchar(100),
	@BirthDate datetime, 
	@TypeSex nvarchar(1),
	@UpdatedOn datetime
as
	update Person
	set name = @Name, 
	birthdate = @BirthDate, 
	typesex= @TypeSex, 
	updatedon= @UpdatedOn
	where id = @Id
	select id, name, birthdate, typesex, createdon, updatedon from Person where id = @Id
GO
