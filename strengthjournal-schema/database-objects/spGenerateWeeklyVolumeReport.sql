CREATE OR ALTER PROCEDURE dbo.spGenerateWeeklyVolumeReport
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