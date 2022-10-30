export interface WorkoutSet {
  id: string;
  exerciseId: string;
  exerciseName: string;
  reps: Number | null;
  targetReps: Number | null;
  weight: Number | null;
  weightUnit: string;
  rpe: Number | null;
}