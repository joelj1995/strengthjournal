export interface WorkoutCreateUpdate {
  title: string;
  entryDateUTC: Date;
  bodyweight: number | null;
  bodyweightUnit: string;
}