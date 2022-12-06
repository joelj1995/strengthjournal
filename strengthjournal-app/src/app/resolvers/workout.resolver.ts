import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { Workout } from '../model/workout';
import { WorkoutService } from '../services/workout.service';

@Injectable({
  providedIn: 'root'
})
export class WorkoutResolver implements Resolve<Workout> {

  constructor(private workout: WorkoutService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Workout> {
    const id = route.paramMap.get('id');
    if (id == null)
      throw 'Null workout id in route';
    return this.workout.getWorkout(id);
  }
}
