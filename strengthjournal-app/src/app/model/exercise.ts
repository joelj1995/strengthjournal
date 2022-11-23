export interface Exercise {
  id: string;
  name: string;
  systemDefined: boolean;
  parentExerciseId: string | null;
}