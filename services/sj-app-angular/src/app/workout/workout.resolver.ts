import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot,
  ActivatedRoute
} from '@angular/router';
import { forkJoin, map, Observable, of, tap } from 'rxjs';
import { Exercise } from '../model/exercise';
import { ResolvedEditWorkout } from '../model/vm/resolved-edit-workout';
import { Workout } from '../model/workout';
import { ExerciseService } from '../services/exercise.service';
import { PathsHelperService } from '../services/paths-helper.service';
import { WorkoutService } from '../services/workout.service';

@Injectable({
  providedIn: 'root'
})
export class WorkoutResolver implements Resolve<ResolvedEditWorkout | null> {

  constructor(
    private workout: WorkoutService, 
    private router: Router,
    private pathsHelper: PathsHelperService,
    private exercise: ExerciseService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ResolvedEditWorkout | null> {
    if (!this.pathsHelper.baseUrlChanged(this.router.url, state.url))
      return of(null);
    const id = route.paramMap.get('id');
    if (id == null)
      throw 'Null workout id in route';
    const workout = this.workout.getWorkout(id);
    const exerciseList = this.exercise.getAllExercises();
    return forkJoin([workout, exerciseList]).pipe(
      map(results => {
        return {
          workout: results[0] as Workout,
          exerciseList: results[1].data as Exercise[]
        } as ResolvedEditWorkout
      })
    );
  }

}
