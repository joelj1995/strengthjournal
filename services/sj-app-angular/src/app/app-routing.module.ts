import { NgModule } from '@angular/core';
import { ResolveData, RouterModule, Routes } from '@angular/router';
import { ContextResolver } from './core/context.resolver';
import { DashboardComponent } from './core/dashboard/dashboard.component';
import { ErrorComponent } from './core/error/error.component';
import { NotFoundComponent } from './core/special/not-found.component';

// context resolver should be added to protected authentication endpoints
// that have a session
const contextResolver: ResolveData = {
  context: ContextResolver
};

const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'dashboard', component: DashboardComponent },
  { 
    path: 'profile', 
    loadChildren: () => import('./profile/profile.module').then(m => m.ProfileModule),
    resolve: contextResolver
  },
  { 
    path: 'exercises', 
    loadChildren: () => import('./exercise/exercise.module').then(m => m.ExerciseModule),
    resolve: contextResolver
  },
  { 
    path: 'workouts', 
    loadChildren: () => import('./workout/workout.module').then(m => m.WorkoutModule),
    resolve: contextResolver
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
