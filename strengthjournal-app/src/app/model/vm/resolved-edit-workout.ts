import { Exercise } from "../exercise";
import { Workout } from "../workout";

export interface ResolvedEditWorkout {
  workout: Workout;
  exerciseList: Exercise[];
}