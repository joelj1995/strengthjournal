import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './core/dashboard/dashboard.component';
import { ErrorComponent } from './core/error/error.component';
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
  {
    path: 'iam',
    loadChildren: () => import('./iam/iam.module').then(m => m.IamModule),
    data: { noNav: true }
  },
  {
    path: 'error',
    component: ErrorComponent,
    data: { noNav: true }
  },
  { 
    path: '**', 
    component: NotFoundComponent,
    data: { noNav: true }
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
