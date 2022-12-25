import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NewExerciseComponent } from './exercise/exercises/new-exercise.component';
import { DashboardComponent } from './core/dashboard/dashboard.component';
import { ListExercisesComponent } from './exercise/exercises/list-exercises.component';
import { NewWorkoutComponent } from './workout/workouts/new-workout.component';
import { EditWorkoutComponent } from './workout/workouts/edit-workout.component';
import { ListWorkoutsComponent } from './workout/workouts/list-workouts.component';
import { EditExerciseComponent } from './exercise/exercises/edit-exercise.component';
import { ViewExerciseComponent } from './exercise/exercises/view-exercise.component';
import { ProfileComponent } from './profile/profile/profile.component';
import { NotFoundComponent } from './core/special/not-found.component';
import { WorkoutResolver } from './workout/workout.resolver';
import { ExerciseResolver } from './exercise/exercise.resolver';
import { EditWorkoutGuard } from './guards/edit-workout.guard';
import { EditExerciseResolver } from './exercise/edit-exercise.resolver';
import { ProfileResolver } from './profile/profile.resolver';

const routes: Routes = [
  { path: '', component: DashboardComponent },
  { 
    path: 'exercises', 
    children: [
      { path: '', component: ListExercisesComponent },
      { 
        path: 'new', 
        component: NewExerciseComponent ,
      },
      { 
        path: ':id/edit', 
        component: EditExerciseComponent,
        resolve: { resolvedEditExercise: EditExerciseResolver }
      },
      {
        path: ':id',
        component: ViewExerciseComponent,
        resolve: { exercise: ExerciseResolver }
      }
    ]
  },
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
  { 
    path: 'profile', 
    component: ProfileComponent ,
    resolve: { profile: ProfileResolver }
  },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
