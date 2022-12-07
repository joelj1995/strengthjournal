import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { Exercise } from '../model/exercise';
import { ExerciseService } from '../services/exercise.service';

@Injectable({
  providedIn: 'root'
})
export class ExerciseResolver implements Resolve<Exercise> {

  constructor(private exercise: ExerciseService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Exercise> {
    const id = route.paramMap.get('id');
    if (id == null)
      throw 'Null workout id in route';
    return this.exercise.getExercise(id);
  }
}
