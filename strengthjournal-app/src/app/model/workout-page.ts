import { Workout } from "./workout";

export interface WorkoutPage {
  perPage: number,
  totalRecords: number,
  currentPage: number,
  data: Workout[]
}