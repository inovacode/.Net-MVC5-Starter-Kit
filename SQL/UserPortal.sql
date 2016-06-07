USE [UserPortal]
GO

/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 8/30/2014 3:08:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 8/30/2014 3:08:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 8/30/2014 3:08:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 8/30/2014 3:08:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 8/30/2014 3:08:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[AppUser]    Script Date: 8/30/2014 3:08:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUser](
	[UserId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[RegistrationDate] [datetime] NOT NULL,
	[ChangeUserId] [nvarchar](128) NOT NULL,
	[ChangeDate] [datetime] NOT NULL,
 CONSTRAINT [PK_AppUser] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[Log]    Script Date: 8/30/2014 3:08:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[LogId] [bigint] IDENTITY(1,1) NOT NULL,
	[LogTypeId] [int] NOT NULL,
	[Descr] [nvarchar](max) NULL,
	[URL] [nvarchar](max) NULL,
	[Page] [nvarchar](max) NULL,
	[LinkToId] [nvarchar](128) NULL,
	[InsertDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Lookup]    Script Date: 8/30/2014 3:08:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Lookup](
	[LookupId] [int] IDENTITY(1,1) NOT NULL,
	[LookupTypeId] [int] NOT NULL,
	[Descr] [varchar](max) NOT NULL,
	[Value] [nvarchar](150) NULL,
	[Sequence] [int] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Lookup] PRIMARY KEY CLUSTERED 
(
	[LookupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LookupType]    Script Date: 8/30/2014 3:08:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LookupType](
	[LookupTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Descr] [varchar](50) NOT NULL,
 CONSTRAINT [PK_LookupType] PRIMARY KEY CLUSTERED 
(
	[LookupTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [idx_AppUser]    Script Date: 8/30/2014 3:08:16 PM ******/
CREATE NONCLUSTERED INDEX [idx_AppUser] ON [dbo].[AppUser]
(
	[StatusId] ASC,
	[FirstName] ASC,
	[LastName] ASC,
	[RegistrationDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [idx_AspNetUsers]    Script Date: 8/30/2014 3:08:16 PM ******/
CREATE NONCLUSTERED INDEX [idx_AspNetUsers] ON [dbo].[AspNetUsers]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [idx_Log]    Script Date: 8/30/2014 3:08:16 PM ******/
CREATE NONCLUSTERED INDEX [idx_Log] ON [dbo].[Log]
(
	[LogTypeId] ASC,
	[LinkToId] ASC,
	[InsertDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [idx_Lookup]    Script Date: 8/30/2014 3:08:16 PM ******/
CREATE NONCLUSTERED INDEX [idx_Lookup] ON [dbo].[Lookup]
(
	[LookupTypeId] ASC,
	[Active] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppUser]  WITH CHECK ADD  CONSTRAINT [FK_AppUser_StatusId] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Lookup] ([LookupId])
GO
ALTER TABLE [dbo].[AppUser] CHECK CONSTRAINT [FK_AppUser_StatusId]
GO
ALTER TABLE [dbo].[AppUser]  WITH CHECK ADD  CONSTRAINT [FK_AppUser_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AppUser] CHECK CONSTRAINT [FK_AppUser_UserId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_UserId]
GO
ALTER TABLE [dbo].[Log]  WITH CHECK ADD  CONSTRAINT [FK_Log_LogType] FOREIGN KEY([LogTypeId])
REFERENCES [dbo].[Lookup] ([LookupId])
GO
ALTER TABLE [dbo].[Log] CHECK CONSTRAINT [FK_Log_LogType]
GO
ALTER TABLE [dbo].[Lookup]  WITH CHECK ADD  CONSTRAINT [FK_Lookup_LookupType] FOREIGN KEY([LookupTypeId])
REFERENCES [dbo].[LookupType] ([LookupTypeId])
GO
ALTER TABLE [dbo].[Lookup] CHECK CONSTRAINT [FK_Lookup_LookupType]
GO


/****** Object:  StoredProcedure [dbo].[AppUser_InsertUpdate]    Script Date: 8/30/2014 3:08:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[AppUser_InsertUpdate]
(
	@UserId int,
	@StatusId int,
	@UserRoleId int,
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Email nvarchar(256),
	@ChangeUserId int
)
As

If Exists (Select UserId From dbo.AppUser (nolock) Where UserId = @UserId)
Begin
	Update [dbo].[AppUser]
	   Set [StatusId] = IsNull(@StatusId, StatusId)
		  ,[FirstName] = @FirstName
		  ,[LastName] = @LastName
		  ,[ChangeUserId] = @ChangeUserId
		  ,[ChangeDate] = GetDate()
	 Where UserId = @UserId

	 Update dbo.AspNetUsers
		Set Email = IsNull(@Email, Email),
			UserName = IsNull(@Email, Email)
	 Where Id = @UserId
End
Else
Begin
	Insert Into [dbo].[AppUser]
			   ([UserId]
			   ,[StatusId]
			   ,[FirstName]
			   ,[LastName]
			   ,[RegistrationDate]
			   ,[ChangeUserId]
			   ,[ChangeDate])
		 Values
			   (@UserId
			   ,@StatusId
			   ,@FirstName
			   ,@LastName
			   ,GetDate()
			   ,@ChangeUserId
			   ,GetDate())
	
	-- Insert User Role
	Insert Into dbo.AspNetUserRoles
		Select @UserId, @UserRoleId
End


-- Return
If (@@Error > 0)
	Select -1 As ErrorCode
Else
	Select @UserId As UserId

GO
/****** Object:  StoredProcedure [dbo].[AspNetRoles_InsertUpdateDelete]    Script Date: 8/30/2014 3:08:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[AspNetRoles_InsertUpdateDelete]
(
	@Id int,
	@Name nvarchar(256),
	@Description nvarchar(max),
	@Delete bit = 0
)
As

If @Delete = 1
Begin
	Delete From AspNetUserRoles Where RoleId = @Id
	Delete From AspNetRoles Where Id = @Id
End
Else
Begin
	If Exists (Select Id From dbo.AspNetRoles (nolock) Where Id = @Id)
	Begin
		Update [dbo].[AspNetRoles]
		   Set [Name] = @Name
			  ,[Description] = @Description
		 Where Id = @Id
	End
	Else
	Begin
		Insert Into [dbo].[AspNetRoles]
				   ([Name]
				   ,[Description])
			 Values
				   (@Name
				   ,@Description)	

		Set @Id = Scope_Identity()
	End
End

-- Return
If (@@Error > 0)
	Select -1 As ErrorCode
Else
	Select @Id As Id

GO
/****** Object:  StoredProcedure [dbo].[AspNetUser_Update]    Script Date: 8/30/2014 3:08:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[AspNetUser_Update]
(
	@UserId int,
	@EmailConfirmed bit = Null
)
As

If Exists (Select Id From dbo.AspNetUsers (nolock) Where Id = @UserId)
Begin
	Update [dbo].[AspNetUsers]
	   Set EmailConfirmed = IsNull(@EmailConfirmed, EmailConfirmed)
	 Where Id = @UserId

End

-- Return
If (@@Error > 0)
	Select -1 As ErrorCode
Else
	Select @UserId As UserId

GO
/****** Object:  StoredProcedure [dbo].[GetQueryData]    Script Date: 8/30/2014 3:08:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Exec GetQueryData 10, 11

CREATE procedure [dbo].[GetQueryData]
(
	@QueryDataTypeId int,
	@Filter varchar(50) = Null
)
As

Set Nocount On;

If @QueryDataTypeId = 5 -- AspNetRoles
Begin
	Select Id, Name, [Description]
	From dbo.AspNetRoles (nolock) 
	Where Id = Case When Convert(int, IsNull(@Filter,'0')) > 0 Then Convert(int, @Filter) Else Id End
	Order By Name Asc
End
Else If @QueryDataTypeId = 6 -- Lookup
Begin
	Select LookupId, Descr, Value
	From dbo.Lookup (nolock)
	Where LookupTypeId = Convert(int, @Filter)
	Order By Sequence, Descr
End
Else If @QueryDataTypeId = 7 -- GetUsersByRoleId
Begin
	Select au.UserId, au.FirstName, au.LastName, u.Email, au.StatusId
	From dbo.AspNetUsers u (nolock) 
		 Join dbo.AppUser au (nolock) on u.Id = au.UserId
		 Join dbo.AspNetUserRoles ur (nolock) on u.Id = ur.UserId
	Where ur.RoleId = Case When Convert(int, IsNull(@Filter,'0')) > 0 Then Convert(int, @Filter) Else ur.RoleId End
	Order By au.FirstName, au.LastName	
End
Else If @QueryDataTypeId = 9 -- GetUserById
Begin
	Select au.UserId, au.FirstName, au.LastName, u.Email, au.StatusId
	From dbo.AspNetUsers u (nolock) 
		 Join dbo.AppUser au (nolock) on u.Id = au.UserId
	Where au.UserId = Case When IsNumeric(@Filter) = 1 Then Convert(int, @Filter) Else au.UserId End
		  And u.Email = Case When IsNumeric(@Filter) = 0 Then @Filter Else u.Email End
End
Else If @QueryDataTypeId = 10 -- GetUserRoles
Begin
	Select r.Id, r.Name, r.[Description]
	From dbo.AspNetUserRoles ur (nolock)
		 Join dbo.AspNetRoles r (nolock) on ur.RoleId = r.Id
	Where ur.UserId = Convert(int, @Filter)
	Order By Name Asc
End
Else
	Select Null
GO
/****** Object:  StoredProcedure [dbo].[Log_Insert]    Script Date: 8/30/2014 3:08:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Log_Insert]
(
	@LogTypeId int,
	@Descr nvarchar(max) = Null,
	@URL nvarchar(max) = Null,
	@Page nvarchar(max) = Null,
	@LinkToId varchar(50) = Null
)

As

Insert Into dbo.[Log]
           ([LogTypeId]
           ,[Descr]
           ,[URL]
           ,[Page]
           ,[LinkToId]
           ,[InsertDate])
     Values
           (@LogTypeId
           ,@Descr
           ,@URL
           ,@Page
           ,@LinkToId
           ,GetDate())


-- RETURN
SELECT Scope_Identity() As LogId

/****** Insert Static Data ******/

INSERT INTO AspNetRoles (Id,Name,Description) VALUES ('1','Admin','Website administrators');
INSERT INTO AspNetRoles (Id,Name,Description) VALUES ('2','Application User','Regular system user');
INSERT INTO AspNetRoles (Id,Name,Description) VALUES ('3','Customer Support','Employee support user');

INSERT INTO LookupType (LookupTypeId,Descr) VALUES ('1','LogType');
INSERT INTO LookupType (LookupTypeId,Descr) VALUES ('2','App User Status');
INSERT INTO LookupType (LookupTypeId,Descr) VALUES ('3','Query Data Type');

INSERT INTO Lookup (LookupId,LookupTypeId,Descr,Value,Sequence,Active) VALUES ('1','1','Site Email',NULL,Null,'1');
INSERT INTO Lookup (LookupId,LookupTypeId,Descr,Value,Sequence,Active) VALUES ('2','1','Site Exception',Null,Null,'1');
INSERT INTO Lookup (LookupId,LookupTypeId,Descr,Value,Sequence,Active) VALUES ('3','2','Active',Null,Null,'1');
INSERT INTO Lookup (LookupId,LookupTypeId,Descr,Value,Sequence,Active) VALUES ('4','2','Inactive',Null,Null,'1');
INSERT INTO Lookup (LookupId,LookupTypeId,Descr,Value,Sequence,Active) VALUES ('5','3','AspNetRoles',Null,Null,'1');
INSERT INTO Lookup (LookupId,LookupTypeId,Descr,Value,Sequence,Active) VALUES ('6','3','Lookup Table',Null,Null,'1');
INSERT INTO Lookup (LookupId,LookupTypeId,Descr,Value,Sequence,Active) VALUES ('7','3','Get Users By RoleId',Null,Null,'1');
INSERT INTO Lookup (LookupId,LookupTypeId,Descr,Value,Sequence,Active) VALUES ('8','3','Get User By Id',Null,Null,'1');
INSERT INTO Lookup (LookupId,LookupTypeId,Descr,Value,Sequence,Active) VALUES ('9','3','Get User Roles',Null,Null,'1');
