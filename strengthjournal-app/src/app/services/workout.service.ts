import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Workout } from '../model/workout';
import { WorkoutCreate } from '../model/workout-create';
import { WorkoutPage } from '../model/workout-page';
import { WorkoutSet } from '../model/workout-set';
import { WorkoutUpdate } from '../model/workout-update';
import { StrengthjournalBaseService } from './strengthjournalbase.service';

@Injectable({
  providedIn: 'root'
})
export class WorkoutService extends StrengthjournalBaseService {

  constructor(http: HttpClient) { super(http); }

  getWorkouts(pageNumber: number, perPage: number): Observable<WorkoutPage> {
    return this.http.get<WorkoutPage>(`${this.BASE_URL}/workouts?pageNumber=${pageNumber}&perPage=${perPage}`);
  }

  createWorkout(workout: WorkoutCreate): Observable<string> {
    return this.http.post<string>(`${this.BASE_URL}/workouts`, workout);
  }

  getWorkout(workoutId: string): Observable<Workout> {
    return this.http.get<Workout>(`${this.BASE_URL}/workouts/${workoutId}`);
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

  updateWorkoutSetSequence(workoutId: string, newSequence: string[]) {
    return this.http.put(`${this.BASE_URL}/workouts/${workoutId}/set-sequence`, { setSequence: newSequence });
  }

  updateWorkout(workoutId: string, workout: WorkoutUpdate) {
    return this.http.put(`${this.BASE_URL}/workouts/${workoutId}`, workout);
  }

  deleteWorkout(workoutId: string) {
    return this.http.delete(`${this.BASE_URL}/workouts/${workoutId}`);
  }
}
