import { ErrorHandler, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { APP_BASE_HREF } from '@angular/common';
import { StrengthjournalBaseService } from './services/strengthjournalbase.service';
import { HttpClientModule } from '@angular/common/http';
import { AuthModule } from '@auth0/auth0-angular';
import { environment } from 'src/environments/environment';

import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthHttpInterceptor } from '@auth0/auth0-angular';
import { NavComponent } from './nav/nav.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { HeaderComponent } from './header/header.component';
import { ListExercisesComponent } from './pages/exercises/list-exercises.component';
import { NewExerciseComponent } from './pages/exercises/new-exercise.component';
import { CardComponent } from './components/card/card.component';
import { NewWorkoutComponent } from './pages/workouts/new-workout.component';
import { EditWorkoutComponent } from './pages/workouts/edit-workout.component';
import { ListWorkoutsComponent } from './pages/workouts/list-workouts.component';
import { TableActionsComponent } from './components/table-actions/table-actions.component';
import { EditExerciseComponent } from './pages/exercises/edit-exercise.component';
import { RPEPipe } from './pipes/rpe-format-pipe';
import { AppErrorHandler } from './app-error-handler';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    DashboardComponent,
    HeaderComponent,
    ListExercisesComponent,
    NewExerciseComponent,
    CardComponent,
    NewWorkoutComponent,
    EditWorkoutComponent,
    ListWorkoutsComponent,
    TableActionsComponent,
    EditExerciseComponent,
    RPEPipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    AuthModule.forRoot({
      domain: 'dev-bs65rtlog25jigd0.us.auth0.com',
      clientId: 'LdMw0S4EL13LvL4SZJOPRCSZo5cZJ3zD',
      audience: 'https://localhost:7080/api',
      httpInterceptor: {
        allowedList: [`${environment.api}*`]
      },
      redirectUri: `${window.location.origin}/app`
    }),
  ],
  providers: [
    { 
      provide: APP_BASE_HREF, 
      useValue: '/app' 
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthHttpInterceptor,
      multi: true,
    },
    {
      provide: ErrorHandler,
      useClass: AppErrorHandler,
    },
    StrengthjournalBaseService],
  bootstrap: [AppComponent]
})
export class AppModule { }
