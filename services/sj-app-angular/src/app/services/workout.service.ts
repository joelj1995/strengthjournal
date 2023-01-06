import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { forkJoin, from, map, mergeMap, Observable, of, switchMap, toArray, withLatestFrom } from 'rxjs';
import { Workout } from '../model/workout';
import { WorkoutActivity, WorkoutActivitySet } from '../model/workout-activity';
import { WorkoutCreate } from '../model/workout-create';
import { WorkoutPage } from '../model/workout-page';
import { WorkoutSet } from '../model/workout-set';
import { WorkoutUpdate } from '../model/workout-update';
import { StrengthjournalBaseService } from './strengthjournalbase.service';
import { DataPage } from '../model/data-page';

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

  getWorkoutActivity(pageNumber: number, perPage: number): Observable<DataPage<WorkoutActivity>> {
    return this.http.get<WorkoutPage>(`${this.BASE_URL}/workouts?pageNumber=${pageNumber}&perPage=${perPage}`)
      .pipe(
        switchMap(res => {
          return forkJoin({
            workouts: from(res.data)
              .pipe(
                mergeMap(workout => this.getWorkout(workout.id)),
                map(workout => this.workoutToActivity(workout)),
                toArray()
              ),
            page: of(res)
          })
        }),
        map(joined => ({
          ...joined.page,
          data: joined.workouts.sort((a, b) => Number(new Date(b.entryDateUTC)) - Number(new Date(a.entryDateUTC)))
        }))
      );
  }

  private workoutToActivity(workout: Workout): WorkoutActivity {
    return {
      id: workout.id,
      title: workout.title,
      entryDateUTC: workout.entryDateUTC,
      sets: this.groupRelatedSets(workout.sets),
      notes: workout.notes
    }
  }

  private groupRelatedSets(sets: WorkoutSet[]): WorkoutActivitySet[] {
    if (sets.length == 0) return [];
    const result: WorkoutActivitySet[] = [];
    let setCount = 1;
    let lastSet = sets[0];
    for (var i = 1; i < sets.length; i++) {
      const currentSet = sets[i];
      if (
        currentSet.exerciseId != lastSet.exerciseId
        || currentSet.reps != lastSet.reps
        || currentSet.weight != lastSet.weight
        || currentSet.weightUnit != lastSet.weightUnit
      ) {
        result.push({
          ...lastSet,
          sets: setCount
        });
        setCount = 1;
      } else {
        setCount += 1;
        if (i == sets.length - 1) {
          result.push({
            ...lastSet,
            sets: setCount
          });
        }
      }
      lastSet = currentSet;
    }
    return result;
  }

}
