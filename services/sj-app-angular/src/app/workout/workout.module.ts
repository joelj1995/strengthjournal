import { NgModule } from '@angular/core';
import { RestTimeComponent } from './rest-time/rest-time.component';
import { WorkoutDetailsEditorComponent } from './workout-details-editor/workout-details-editor.component';
import { ListWorkoutsComponent } from './list-workouts/list-workouts.component';
import { EditWorkoutComponent } from './edit-workout/edit-workout.component';
import { NewWorkoutComponent } from './new-workout/new-workout.component';
import { RouterModule, Routes } from '@angular/router';
import { EditWorkoutGuard } from './edit-workout.guard';
import { WorkoutResolver } from './workout.resolver';
import { SharedModule } from '../shared/shared.module';
import { SetEditorComponent } from './edit-workout/set-editor.component';

const routes: Routes = [
  { path: '', component: ListWorkoutsComponent },
  { path: 'new', component: NewWorkoutComponent },
  {
    path: ':id/edit',
    component: EditWorkoutComponent,
    canDeactivate: [ EditWorkoutGuard ],
    resolve: { workout: WorkoutResolver }
  }
]

@NgModule({
  declarations: [
    NewWorkoutComponent,
    EditWorkoutComponent,
    ListWorkoutsComponent,
    WorkoutDetailsEditorComponent,
    RestTimeComponent,
    SetEditorComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    SharedModule
  ]
})
export class WorkoutModule { }
