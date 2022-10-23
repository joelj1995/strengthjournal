import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { DevLoginComponent } from './pages/dev-login/dev-login.component';
import { ListExercisesComponent } from './pages/exercises/list-exercises.component';

const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'login', component: DevLoginComponent },
  { path: 'exercises', component: ListExercisesComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
