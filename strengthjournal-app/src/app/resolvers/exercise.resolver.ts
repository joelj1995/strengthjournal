import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of, tap } from 'rxjs';
import { Exercise } from '../model/exercise';
import { ExerciseService } from '../services/exercise.service';
import { SpinnerService } from '../services/spinner.service';

@Injectable({
  providedIn: 'root'
})
export class ExerciseResolver implements Resolve<Exercise> {

  constructor(private exercise: ExerciseService, private spinner: SpinnerService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Exercise> {
    this.spinner.setSpinnerEnabled(true);
    const id = route.paramMap.get('id');
    if (id == null)
      throw 'Null workout id in route';
    return this.exercise.getExercise(id).pipe(tap(() => this.spinner.setSpinnerEnabled(false)));
  }
}
