export interface WorkoutActivity {
  id: string
  title: string
  entryDateUTC: Date
  sets: WorkoutActivitySet[]
  notes: string
}

export interface WorkoutActivitySet {
  exerciseName: string
  weight: number | null
  weightUnit: string
  sets: number
  reps: number | null
}