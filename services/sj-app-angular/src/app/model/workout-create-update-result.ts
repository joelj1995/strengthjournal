import { WorkoutCreate } from "./workout-create";

export interface WorkoutCreateUpdateResult extends WorkoutCreate {
  id: string;
}