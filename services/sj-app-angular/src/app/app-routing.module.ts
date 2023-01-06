import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './core/dashboard/dashboard.component';
import { NotFoundComponent } from './core/special/not-found.component';

const routes: Routes = [
  { path: '', component: DashboardComponent },
  { 
    path: 'profile', 
    loadChildren: () => import('./profile/profile.module').then(m => m.ProfileModule)
  },
  { 
    path: 'exercises', 
    loadChildren: () => import('./exercise/exercise.module').then(m => m.ExerciseModule)
  },
  { 
    path: 'workouts', 
    loadChildren: () => import('./workout/workout.module').then(m => m.WorkoutModule)
  },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
