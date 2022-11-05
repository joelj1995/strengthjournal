import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Workout } from '../model/workout';
import { WorkoutCreate } from '../model/workout-create';
import { WorkoutSet } from '../model/workout-set';
import { StrengthjournalBaseService } from './strengthjournalbase.service';

@Injectable({
  providedIn: 'root'
})
export class WorkoutService extends StrengthjournalBaseService {

  constructor(http: HttpClient) { super(http); }

  getWorkouts(): Observable<Workout[]> {
    return this.http.get<Workout[]>(`${this.BASE_URL}/workouts`);
  }

  createWorkout(workout: WorkoutCreate): Observable<string> {
    return this.http.post<string>(`${this.BASE_URL}/workouts`, workout);
  }

  getWorkoutSets(workoutId: string): Observable<WorkoutSet[]> {
    return this.http.get<WorkoutSet[]>(`${this.BASE_URL}/workouts/${workoutId}/sets`);
  }

  syncSet(workoutId: string, set: WorkoutSet) {
    return this.http.put<void>(`${this.BASE_URL}/workouts/${workoutId}/sets`, set);
  }

  deleteSet(workoutId: string, setId: string) {
    return this.http.delete(`${this.BASE_URL}/workouts/${workoutId}/sets/${setId}`);
  }

  deleteWorkout(workoutId: string) {
    return this.http.delete(`${this.BASE_URL}/workouts/${workoutId}`);
  }
}
