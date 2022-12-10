import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { map, Observable, of } from 'rxjs';
import { Exercise } from '../model/exercise';
import { ResolvedEditExercise } from '../model/resolved-edit-exercise';
import { ExerciseService } from '../services/exercise.service';

@Injectable({
  providedIn: 'root'
})
export class EditExerciseResolver implements Resolve<ResolvedEditExercise> {

  constructor(private exercise: ExerciseService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ResolvedEditExercise> {
    const id = route.paramMap.get('id');
    return this.exercise.getAllExercises().pipe(map(page => ({
      exercise: page.data.find(e => e.id == id) as Exercise,
      exerciseList: page.data.filter(e => e.parentExerciseId == null && e.id != id)
    })));
  }
}
