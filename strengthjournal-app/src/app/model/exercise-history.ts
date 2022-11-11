export interface ExerciseHistory {
  EntryDateUTC: Date;
  reps: number | null;
  targetReps: number | null;
  weight: number | null;
  weightUnit: string;
  rpe: number | null;
}