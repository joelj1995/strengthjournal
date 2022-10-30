import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NewExerciseComponent } from './pages/exercises/new-exercise.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { DevLoginComponent } from './pages/dev-login/dev-login.component';
import { ListExercisesComponent } from './pages/exercises/list-exercises.component';
import { NewWorkoutComponent } from './pages/workouts/new-workout.component';

const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'login', component: DevLoginComponent },
  { path: 'exercises', component: ListExercisesComponent },
  { path: 'exercises/new', component: NewExerciseComponent },
  { path: 'workouts/new', component: NewWorkoutComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
