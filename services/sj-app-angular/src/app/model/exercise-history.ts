export interface ExerciseHistory {
  entryDateUTC: Date;
  bodyWeightKg: number | null;
  bodyWeightLbs: number | null;
  weightKg: number | null;
  weightLbs: number | null;
  reps: number | null;
  targetReps: number | null;
  rpe: number | null;
}