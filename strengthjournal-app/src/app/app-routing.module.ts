import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './core/dashboard/dashboard.component';
import { NewWorkoutComponent } from './workout/workouts/new-workout.component';
import { EditWorkoutComponent } from './workout/workouts/edit-workout.component';
import { ListWorkoutsComponent } from './workout/workouts/list-workouts.component';
import { NotFoundComponent } from './core/special/not-found.component';
import { WorkoutResolver } from './workout/workout.resolver';
import { EditWorkoutGuard } from './workout/edit-workout.guard';

const routes: Routes = [
  { path: '', component: DashboardComponent },
  {
    path: 'workouts',
    children: [
      { path: '', component: ListWorkoutsComponent },
      { path: 'new', component: NewWorkoutComponent },
      {
        path: ':id/edit',
        component: EditWorkoutComponent,
        canDeactivate: [ EditWorkoutGuard ],
        resolve: { workout: WorkoutResolver }
      }
    ]
  },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
