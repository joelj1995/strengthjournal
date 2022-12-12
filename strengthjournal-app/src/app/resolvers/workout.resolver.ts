import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot,
  ActivatedRoute
} from '@angular/router';
import { Observable, of, tap } from 'rxjs';
import { Workout } from '../model/workout';
import { WorkoutService } from '../services/workout.service';

@Injectable({
  providedIn: 'root'
})
export class WorkoutResolver implements Resolve<Workout | null> {

  constructor(private workout: WorkoutService, private router: Router) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Workout | null> {
    const sourcePage = this.getBasePath(this.router.url);
    const targetPage = this.getBasePath(state.url);
    if (sourcePage == targetPage)
      return of(null);
    const id = route.paramMap.get('id');
    if (id == null)
      throw 'Null workout id in route';
    return this.workout.getWorkout(id);
  }

  getBasePath(url: string): string {
    const optionalSeparatorIdx = url.indexOf(';');
    const substringEnd = optionalSeparatorIdx == -1 ? url.length : optionalSeparatorIdx;
    return url.substring(0, substringEnd);
  }

}
