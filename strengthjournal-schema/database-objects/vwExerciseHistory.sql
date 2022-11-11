CREATE OR ALTER VIEW vwExerciseHistory AS

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