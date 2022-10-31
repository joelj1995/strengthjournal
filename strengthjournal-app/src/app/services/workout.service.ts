import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Workout } from '../model/workout';
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

  createWorkoutNow(): Observable<string> {
    return this.http.post<string>(`${this.BASE_URL}/workouts`, { startDate: new Date().toISOString() });
  }

  getWorkoutSets(workoutId: string): Observable<WorkoutSet[]> {
    return this.http.get<WorkoutSet[]>(`${this.BASE_URL}/workouts/${workoutId}/sets`);
  }

  syncSet(workoutId: string, set: WorkoutSet) {
    return this.http.put<void>(`${this.BASE_URL}/workouts/${workoutId}/sets`, set);
  }

}
