import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NewExerciseComponent } from './pages/exercises/new-exercise.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { ListExercisesComponent } from './pages/exercises/list-exercises.component';
import { NewWorkoutComponent } from './pages/workouts/new-workout.component';
import { EditWorkoutComponent } from './pages/workouts/edit-workout.component';
import { ListWorkoutsComponent } from './pages/workouts/list-workouts.component';
import { EditExerciseComponent } from './pages/exercises/edit-exercise.component';
import { ViewExerciseComponent } from './pages/exercises/view-exercise.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { NotFoundComponent } from './pages/special/not-found.component';

const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'exercises', component: ListExercisesComponent },
  { path: 'exercises/new', component: NewExerciseComponent },
  { path: 'exercises/:id/edit', component: EditExerciseComponent },
  { path: 'exercises/:id', component: ViewExerciseComponent },
  { path: 'workouts', component: ListWorkoutsComponent },
  { path: 'workouts/new', component: NewWorkoutComponent },
  { path: 'workouts/:id/edit', component: EditWorkoutComponent },
  { path: 'profile', component: ProfileComponent },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
