import { Workout } from "./workout";

export interface WorkoutPage {
  perPage: number,
  totalPages: number,
  currentPage: number,
  workouts: Workout[]
}