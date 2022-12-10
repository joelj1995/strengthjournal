import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { EditWorkoutComponent } from '../pages/workouts/edit-workout.component';

@Injectable({
  providedIn: 'root'
})
export class EditWorkoutGuard implements CanDeactivate<EditWorkoutComponent> {
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return true;
  }

  canDeactivate(component: EditWorkoutComponent, currentRoute: ActivatedRouteSnapshot, currentState: RouterStateSnapshot, nextState?: RouterStateSnapshot | undefined): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    if (component.anyUnsavedChanges()) {
      return confirm('You have a set entered that hasn\'t been saved. Navigate away and lose changes?')
    }
    return true;
  }
  
}
