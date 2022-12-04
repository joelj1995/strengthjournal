export interface WorkoutUpdate {
  title: string;
  entryDateUTC: Date;
  bodyweight: number | null;
  bodyweightUnit: string;
  notes: string;
}