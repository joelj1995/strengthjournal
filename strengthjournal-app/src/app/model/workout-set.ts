export interface WorkoutSet {
  id: string;
  exerciseId: string;
  exerciseName: string;
  reps: number | null;
  targetReps: number | null;
  weight: number | null;
  weightUnit: string;
  rpe: number | null;
}