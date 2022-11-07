import { WorkoutSet } from "./workout-set";

export interface Workout {
  id: string;
  title: string;
  entryDateUTC: Date;
  bodyweight: number | null;
  sets: WorkoutSet[]
}