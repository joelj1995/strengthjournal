import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of, tap } from 'rxjs';
import { Workout } from '../model/workout';
import { SpinnerService } from '../services/spinner.service';
import { WorkoutService } from '../services/workout.service';

@Injectable({
  providedIn: 'root'
})
export class WorkoutResolver implements Resolve<Workout | null> {

  constructor(private workout: WorkoutService, private spinner: SpinnerService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Workout | null> {
    if (route.paramMap.get('bypassResolver'))
      return of(null);
    this.spinner.setSpinnerEnabled(true);
    const id = route.paramMap.get('id');
    if (id == null)
      throw 'Null workout id in route';
    return this.workout.getWorkout(id).pipe(tap(() => this.spinner.setSpinnerEnabled(false)));
  }
}
