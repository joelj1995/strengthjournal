CREATE OR ALTER PROCEDURE dbo.spGenerateWeeklySBDTonnageReport
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