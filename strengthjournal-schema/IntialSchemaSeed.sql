SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Exercises](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[ParentExerciseId] [uniqueidentifier] NULL,
	[CreatedByUserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Exercises] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkoutLogEntries]    Script Date: 2022-12-10 2:17:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkoutLogEntries](
	[Id] [uniqueidentifier] NOT NULL,
	[EntryDateUTC] [datetime2](7) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[BodyWeightPIT] [decimal](18, 2) NULL,
	[Notes] [nvarchar](max) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[BodyWeightPITUnitId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_WorkoutLogEntries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkoutLogEntrySets]    Script Date: 2022-12-10 2:17:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkoutLogEntrySets](
	[Id] [uniqueidentifier] NOT NULL,
	[WorkoutLogEntryId] [uniqueidentifier] NOT NULL,
	[ExerciseId] [uniqueidentifier] NOT NULL,
	[Reps] [int] NULL,
	[TargetReps] [int] NULL,
	[Weight] [decimal](18, 2) NULL,
	[RPE] [int] NULL,
	[Sequence] [int] NOT NULL,
	[WeightUnitId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_WorkoutLogEntrySets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WeightUnits]    Script Date: 2022-12-10 2:17:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WeightUnits](
	[Id] [uniqueidentifier] NOT NULL,
	[FullName] [nvarchar](255) NOT NULL,
	[Abbreviation] [nvarchar](20) NOT NULL,
	[RatioToKg] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_WeightUnits] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwExerciseHistory]    Script Date: 2022-12-10 2:17:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[vwExerciseHistory] AS

SELECT
	wle.UserId,
	e.Id [ExerciseId],
	wle.Id [WorkoutId],
	wle.EntryDateUTC,
	[wle].BodyWeightPIT / COALESCE(bwu.RatioToKg, 1) [BodyWeightKg],
	[wle].BodyWeightPIT  / (COALESCE(bwu.RatioToKg, 1) / (SELECT TOP 1 RatioToKg FROM dbo.WeightUnits WHERE Abbreviation = 'lbs')) [BodyWeightLbs],
	[set].[Weight] / COALESCE(wu.RatioToKg, 1) [WeightKg],
	[set].[Weight] / (COALESCE(wu.RatioToKg, 1) / (SELECT TOP 1 RatioToKg FROM dbo.WeightUnits WHERE Abbreviation = 'lbs')) [WeightLbs],
	[set].Reps,
	[set].TargetReps,
	[set].RPE
FROM 
	dbo.WorkoutLogEntrySets [set]
	INNER JOIN dbo.Exercises e ON [set].ExerciseId = e.Id
	INNER JOIN dbo.WorkoutLogEntries wle ON [set].WorkoutLogEntryId = wle.Id
	LEFT JOIN dbo.WeightUnits wu ON [set].WeightUnitId = wu.Id
	LEFT JOIN dbo.WeightUnits bwu ON [wle].BodyWeightPITUnitId = bwu.Id

GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2022-12-10 2:17:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppErrors]    Script Date: 2022-12-10 2:17:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppErrors](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ApiErrorTraceId] [uniqueidentifier] NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_AppErrors] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [CX_AppErrors]    Script Date: 2022-12-10 2:17:03 PM ******/
CREATE CLUSTERED INDEX [CX_AppErrors] ON [dbo].[AppErrors]
(
	[DateCreated] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 2022-12-10 2:17:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[Code] [nvarchar](2) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2022-12-10 2:17:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[ExternalId] [nvarchar](255) NOT NULL,
	[ConsentCEM] [bit] NOT NULL,
	[Email] [nvarchar](320) NOT NULL,
	[PreferredWeightUnitId] [uniqueidentifier] NOT NULL,
	[UserCountryCode] [nvarchar](2) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [CX_Users]    Script Date: 2022-12-10 2:17:03 PM ******/
CREATE CLUSTERED INDEX [CX_Users] ON [dbo].[Users]
(
	[ExternalId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Exercises_CreatedByUserId]    Script Date: 2022-12-10 2:17:03 PM ******/
CREATE NONCLUSTERED INDEX [IX_Exercises_CreatedByUserId] ON [dbo].[Exercises]
(
	[CreatedByUserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_PreferredWeightUnitId]    Script Date: 2022-12-10 2:17:03 PM ******/
CREATE NONCLUSTERED INDEX [IX_Users_PreferredWeightUnitId] ON [dbo].[Users]
(
	[PreferredWeightUnitId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_UserCountryCode]    Script Date: 2022-12-10 2:17:03 PM ******/
CREATE NONCLUSTERED INDEX [IX_Users_UserCountryCode] ON [dbo].[Users]
(
	[UserCountryCode] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_WorkoutLogEntries_BodyWeightPITUnitId]    Script Date: 2022-12-10 2:17:03 PM ******/
CREATE NONCLUSTERED INDEX [IX_WorkoutLogEntries_BodyWeightPITUnitId] ON [dbo].[WorkoutLogEntries]
(
	[BodyWeightPITUnitId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_WorkoutLogEntries_UserId]    Script Date: 2022-12-10 2:17:03 PM ******/
CREATE NONCLUSTERED INDEX [IX_WorkoutLogEntries_UserId] ON [dbo].[WorkoutLogEntries]
(
	[UserId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_WorkoutLogEntries_UserID_EntryDateUTC]    Script Date: 2022-12-10 2:17:03 PM ******/
CREATE NONCLUSTERED INDEX [IX_WorkoutLogEntries_UserID_EntryDateUTC] ON [dbo].[WorkoutLogEntries]
(
	[UserId] ASC,
	[EntryDateUTC] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_WorkoutLogEntrySets_ExerciseId]    Script Date: 2022-12-10 2:17:03 PM ******/
CREATE NONCLUSTERED INDEX [IX_WorkoutLogEntrySets_ExerciseId] ON [dbo].[WorkoutLogEntrySets]
(
	[ExerciseId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_WorkoutLogEntrySets_WeightUnitId]    Script Date: 2022-12-10 2:17:03 PM ******/
CREATE NONCLUSTERED INDEX [IX_WorkoutLogEntrySets_WeightUnitId] ON [dbo].[WorkoutLogEntrySets]
(
	[WeightUnitId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_WorkoutLogEntrySets_WorkoutLogEntryId]    Script Date: 2022-12-10 2:17:03 PM ******/
CREATE NONCLUSTERED INDEX [IX_WorkoutLogEntrySets_WorkoutLogEntryId] ON [dbo].[WorkoutLogEntrySets]
(
	[WorkoutLogEntryId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (CONVERT([bit],(0))) FOR [ConsentCEM]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (N'') FOR [Email]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ('bf8df35b-2f45-4a79-a49c-d3aca4a12cd6') FOR [PreferredWeightUnitId]
GO
ALTER TABLE [dbo].[WeightUnits] ADD  DEFAULT ((0.0)) FOR [RatioToKg]
GO
ALTER TABLE [dbo].[WorkoutLogEntries] ADD  DEFAULT (N'') FOR [Notes]
GO
ALTER TABLE [dbo].[WorkoutLogEntries] ADD  DEFAULT (N'') FOR [Title]
GO
ALTER TABLE [dbo].[WorkoutLogEntrySets] ADD  DEFAULT ((0)) FOR [Sequence]
GO
ALTER TABLE [dbo].[Exercises]  WITH CHECK ADD  CONSTRAINT [FK_Exercises_Exercises_ParentExerciseID] FOREIGN KEY([ParentExerciseId])
REFERENCES [dbo].[Exercises] ([Id])
GO
ALTER TABLE [dbo].[Exercises] CHECK CONSTRAINT [FK_Exercises_Exercises_ParentExerciseID]
GO
ALTER TABLE [dbo].[Exercises]  WITH CHECK ADD  CONSTRAINT [FK_Exercises_Users_CreatedByUserId] FOREIGN KEY([CreatedByUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Exercises] CHECK CONSTRAINT [FK_Exercises_Users_CreatedByUserId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Countries_UserCountryCode] FOREIGN KEY([UserCountryCode])
REFERENCES [dbo].[Countries] ([Code])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Countries_UserCountryCode]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_WeightUnits_PreferredWeightUnitId] FOREIGN KEY([PreferredWeightUnitId])
REFERENCES [dbo].[WeightUnits] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_WeightUnits_PreferredWeightUnitId]
GO
ALTER TABLE [dbo].[WorkoutLogEntries]  WITH CHECK ADD  CONSTRAINT [FK_WorkoutLogEntries_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WorkoutLogEntries] CHECK CONSTRAINT [FK_WorkoutLogEntries_Users_UserId]
GO
ALTER TABLE [dbo].[WorkoutLogEntries]  WITH CHECK ADD  CONSTRAINT [FK_WorkoutLogEntries_WeightUnits_BodyWeightPITUnitId] FOREIGN KEY([BodyWeightPITUnitId])
REFERENCES [dbo].[WeightUnits] ([Id])
GO
ALTER TABLE [dbo].[WorkoutLogEntries] CHECK CONSTRAINT [FK_WorkoutLogEntries_WeightUnits_BodyWeightPITUnitId]
GO
ALTER TABLE [dbo].[WorkoutLogEntrySets]  WITH CHECK ADD  CONSTRAINT [FK_WorkoutLogEntrySets_Exercises_ExerciseId] FOREIGN KEY([ExerciseId])
REFERENCES [dbo].[Exercises] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WorkoutLogEntrySets] CHECK CONSTRAINT [FK_WorkoutLogEntrySets_Exercises_ExerciseId]
GO
ALTER TABLE [dbo].[WorkoutLogEntrySets]  WITH CHECK ADD  CONSTRAINT [FK_WorkoutLogEntrySets_WeightUnits_WeightUnitId] FOREIGN KEY([WeightUnitId])
REFERENCES [dbo].[WeightUnits] ([Id])
GO
ALTER TABLE [dbo].[WorkoutLogEntrySets] CHECK CONSTRAINT [FK_WorkoutLogEntrySets_WeightUnits_WeightUnitId]
GO
ALTER TABLE [dbo].[WorkoutLogEntrySets]  WITH CHECK ADD  CONSTRAINT [FK_WorkoutLogEntrySets_WorkoutLogEntries_WorkoutLogEntryId] FOREIGN KEY([WorkoutLogEntryId])
REFERENCES [dbo].[WorkoutLogEntries] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WorkoutLogEntrySets] CHECK CONSTRAINT [FK_WorkoutLogEntrySets_WorkoutLogEntries_WorkoutLogEntryId]
GO
/****** Object:  StoredProcedure [dbo].[spGenerateWeeklySBDTonnageReport]    Script Date: 2022-12-10 2:17:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[spGenerateWeeklySBDTonnageReport]
	@UserId UNIQUEIDENTIFIER
AS
BEGIN

	DECLARE @EightWeeksAgo DATE = (SELECT DATEADD(week, -7, GETDATE()));
	DECLARE @StartDate DATE = DATEADD(day, -(DATEPART(weekday, @EightWeeksAgo)-1), @EightWeeksAgo);

	DECLARE @SquatID UNIQUEIDENTIFIER = 'BD99B901-26EB-4F12-90CA-91D6A7CC673D';
	DECLARE @BenchID UNIQUEIDENTIFIER = 'F06FDF7F-76C0-4C67-8C3A-994009E75D5A';
	DECLARE @DeadliftID UNIQUEIDENTIFIER = '84E827A0-CA29-42F8-9B82-C0A79886FC30';

	CREATE TABLE #Weeks (
		WeekStart DATE
	)

	INSERT INTO 
		#Weeks (WeekStart)
	VALUES
		(@StartDate),
		(DATEADD(week, 1, @StartDate)),
		(DATEADD(week, 2, @StartDate)),
		(DATEADD(week, 3, @StartDate)),
		(DATEADD(week, 4, @StartDate)),
		(DATEADD(week, 5, @StartDate)),
		(DATEADD(week, 6, @StartDate)),
		(DATEADD(week, 7, @StartDate));

	SELECT
		w.WeekStart,
		(SELECT 
			COALESCE(SUM([set].[Weight] * [set].Reps), 0)
			FROM 
			WorkoutLogEntries [entry]
			INNER JOIN WorkoutLogEntrySets [set] ON [set].WorkoutLogEntryId = [entry].Id
			LEFT JOIN Exercises e ON e.Id = [set].ExerciseId
		WHERE 
			DATEPART(week, [entry].EntryDateUTC) = DATEPART(week, w.WeekStart)
			AND ([set].ExerciseId = @SquatID OR e.ParentExerciseId = @SquatID)
			AND [entry].UserId = @UserID) [SquatTonnage],
		(SELECT 
			COALESCE(SUM([set].[Weight] * [set].Reps), 0)
			FROM 
			WorkoutLogEntries [entry]
			INNER JOIN WorkoutLogEntrySets [set] ON [set].WorkoutLogEntryId = [entry].Id
			LEFT JOIN Exercises e ON e.Id = [set].ExerciseId
		WHERE 
			DATEPART(week, [entry].EntryDateUTC) = DATEPART(week, w.WeekStart)
			AND ([set].ExerciseId = @BenchID OR e.ParentExerciseId = @BenchID)
			AND [entry].UserId = @UserID) [BenchTonnage],
		(SELECT 
			COALESCE(SUM([set].[Weight] * [set].Reps), 0)
			FROM 
			WorkoutLogEntries [entry]
			INNER JOIN WorkoutLogEntrySets [set] ON [set].WorkoutLogEntryId = [entry].Id
			LEFT JOIN Exercises e ON e.Id = [set].ExerciseId
		WHERE 
			DATEPART(week, [entry].EntryDateUTC) = DATEPART(week, w.WeekStart)
			AND ([set].ExerciseId = @DeadliftID OR e.ParentExerciseId = @DeadliftID)
			AND [entry].UserId = @UserID) [DeadliftTonnage]

	FROM
		#Weeks w;

	DROP TABLE IF EXISTS #Weeks;

END
GO
/****** Object:  StoredProcedure [dbo].[spGenerateWeeklyVolumeReport]    Script Date: 2022-12-10 2:17:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[spGenerateWeeklyVolumeReport]
	@UserId UNIQUEIDENTIFIER
AS
BEGIN

	DECLARE @EightWeeksAgo DATE = (SELECT DATEADD(week, -7, GETDATE()));
	DECLARE @StartDate DATE = DATEADD(day, -(DATEPART(weekday, @EightWeeksAgo)-1), @EightWeeksAgo);

	CREATE TABLE #Weeks (
		WeekStart DATE
	)

	INSERT INTO 
		#Weeks (WeekStart)
	VALUES
		(@StartDate),
		(DATEADD(week, 1, @StartDate)),
		(DATEADD(week, 2, @StartDate)),
		(DATEADD(week, 3, @StartDate)),
		(DATEADD(week, 4, @StartDate)),
		(DATEADD(week, 5, @StartDate)),
		(DATEADD(week, 6, @StartDate)),
		(DATEADD(week, 7, @StartDate));

	SELECT
		w.WeekStart,
		(SELECT 
			COUNT(*) 
		 FROM 
			WorkoutLogEntries [entry]
			INNER JOIN WorkoutLogEntrySets [set]  ON [set].WorkoutLogEntryId = [entry].Id
		WHERE 
			DATEPART(week, [entry].EntryDateUTC) = DATEPART(week, w.WeekStart) 
			AND [entry].UserId = @UserID) [NumberOfSets]
	FROM
		#Weeks w;

	DROP TABLE IF EXISTS #Weeks;

END