/****** Object:  Table [dbo].[Exercises]    Script Date: 2022-11-11 7:22:48 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkoutLogEntries]    Script Date: 2022-11-11 7:22:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkoutLogEntries](
	[Id] [uniqueidentifier] NOT NULL,
	[EntryDateUTC] [datetime2](7) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[BodyWeightPIT] [bigint] NULL,
	[Notes] [nvarchar](max) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[BodyWeightPITUnitId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_WorkoutLogEntries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkoutLogEntrySets]    Script Date: 2022-11-11 7:22:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkoutLogEntrySets](
	[Id] [uniqueidentifier] NOT NULL,
	[WorkoutLogEntryId] [uniqueidentifier] NOT NULL,
	[ExerciseId] [uniqueidentifier] NOT NULL,
	[Reps] [bigint] NULL,
	[TargetReps] [bigint] NULL,
	[Weight] [decimal](18, 2) NULL,
	[RPE] [bigint] NULL,
	[Sequence] [int] NOT NULL,
	[WeightUnitId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_WorkoutLogEntrySets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WeightUnits]    Script Date: 2022-11-11 7:22:48 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwExerciseHistory]    Script Date: 2022-11-11 7:22:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[vwExerciseHistory] AS

SELECT
	wle.UserId,
	e.Id [ExerciseId],
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
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2022-11-11 7:22:48 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2022-11-11 7:22:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[ExternalId] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221022205710_Initial', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221022212046_CreateExerciseEntity', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221022212332_SeedExercises', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221023194230_IndexHandle', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221030010504_CreateWorkoutLogEntryEntity', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221030185810_MakeRPENotNull', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221030235951_ExtendWorkoutEntity', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221103014630_AddSequenceFieldToWorkoutLogEntrySet', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221106181058_SimplifyWeightTracking', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221107014324_TrackBodyweightUnit', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221111191312_AddKgRatioColumnToWeightUnit', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221111204831_CreateExerciseHistoryView', N'6.0.10')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221112001853_RenameHandleExternalId', N'6.0.10')
GO
INSERT [dbo].[Exercises] ([Id], [Name], [ParentExerciseId], [CreatedByUserId]) VALUES (N'bd99b901-26eb-4f12-90ca-91d6a7cc673d', N'Squat', NULL, NULL)
GO
INSERT [dbo].[Exercises] ([Id], [Name], [ParentExerciseId], [CreatedByUserId]) VALUES (N'f06fdf7f-76c0-4c67-8c3a-994009e75d5a', N'Bench Press', NULL, NULL)
GO
INSERT [dbo].[Exercises] ([Id], [Name], [ParentExerciseId], [CreatedByUserId]) VALUES (N'84e827a0-ca29-42f8-9b82-c0a79886fc30', N'Deadlift', NULL, NULL)
GO
INSERT [dbo].[WeightUnits] ([Id], [FullName], [Abbreviation], [RatioToKg]) VALUES (N'4bc96550-f274-4a90-978b-92a398f8c49d', N'Kilograms', N'kg', CAST(1.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[WeightUnits] ([Id], [FullName], [Abbreviation], [RatioToKg]) VALUES (N'bf8df35b-2f45-4a79-a49c-d3aca4a12cd6', N'Pounds', N'lbs', CAST(2.20 AS Decimal(18, 2)))
GO
/****** Object:  Index [IX_Exercises_CreatedByUserId]    Script Date: 2022-11-11 7:22:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_Exercises_CreatedByUserId] ON [dbo].[Exercises]
(
	[CreatedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_ExternalId]    Script Date: 2022-11-11 7:22:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_Users_ExternalId] ON [dbo].[Users]
(
	[ExternalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_WorkoutLogEntries_BodyWeightPITUnitId]    Script Date: 2022-11-11 7:22:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_WorkoutLogEntries_BodyWeightPITUnitId] ON [dbo].[WorkoutLogEntries]
(
	[BodyWeightPITUnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_WorkoutLogEntries_UserId]    Script Date: 2022-11-11 7:22:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_WorkoutLogEntries_UserId] ON [dbo].[WorkoutLogEntries]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_WorkoutLogEntrySets_ExerciseId]    Script Date: 2022-11-11 7:22:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_WorkoutLogEntrySets_ExerciseId] ON [dbo].[WorkoutLogEntrySets]
(
	[ExerciseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_WorkoutLogEntrySets_WeightUnitId]    Script Date: 2022-11-11 7:22:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_WorkoutLogEntrySets_WeightUnitId] ON [dbo].[WorkoutLogEntrySets]
(
	[WeightUnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_WorkoutLogEntrySets_WorkoutLogEntryId]    Script Date: 2022-11-11 7:22:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_WorkoutLogEntrySets_WorkoutLogEntryId] ON [dbo].[WorkoutLogEntrySets]
(
	[WorkoutLogEntryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[WeightUnits] ADD  DEFAULT ((0.0)) FOR [RatioToKg]
GO
ALTER TABLE [dbo].[WorkoutLogEntries] ADD  DEFAULT (N'') FOR [Notes]
GO
ALTER TABLE [dbo].[WorkoutLogEntries] ADD  DEFAULT (N'') FOR [Title]
GO
ALTER TABLE [dbo].[WorkoutLogEntrySets] ADD  DEFAULT ((0)) FOR [Sequence]
GO
ALTER TABLE [dbo].[Exercises]  WITH CHECK ADD  CONSTRAINT [FK_Exercises_Users_CreatedByUserId] FOREIGN KEY([CreatedByUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Exercises] CHECK CONSTRAINT [FK_Exercises_Users_CreatedByUserId]
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