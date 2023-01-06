export interface WorkoutCreate {
  title: string;
  entryDateUTC: Date;
  bodyweight: number | null;
  bodyweightUnit: string;
}