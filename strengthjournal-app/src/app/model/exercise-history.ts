export interface ExerciseHistory {
  entryDateUTC: Date;
  reps: number | null;
  targetReps: number | null;
  weight: number | null;
  weightUnit: string;
  rpe: number | null;
}