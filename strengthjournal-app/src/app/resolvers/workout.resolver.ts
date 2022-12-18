import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot,
  ActivatedRoute
} from '@angular/router';
import { Observable, of, tap } from 'rxjs';
import { Workout } from '../model/workout';
import { PathsHelperService } from '../services/paths-helper.service';
import { WorkoutService } from '../services/workout.service';

@Injectable({
  providedIn: 'root'
})
export class WorkoutResolver implements Resolve<Workout | null> {

  constructor(
    private workout: WorkoutService, 
    private router: Router,
    private pathsHelper: PathsHelperService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Workout | null> {
    if (!this.pathsHelper.baseUrlChanged(this.router.url, state.url))
      return of(null);
    const id = route.paramMap.get('id');
    if (id == null)
      throw 'Null workout id in route';
    return this.workout.getWorkout(id);
  }

}
