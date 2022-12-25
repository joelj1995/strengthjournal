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
import { NavComponent } from './components/nav/nav.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { HeaderComponent } from './components/header/header.component';
import { ListExercisesComponent } from './exercise/exercises/list-exercises.component';
import { NewExerciseComponent } from './exercise/exercises/new-exercise.component';
import { CardComponent } from './components/card/card.component';
import { NewWorkoutComponent } from './workout/workouts/new-workout.component';
import { EditWorkoutComponent } from './workout/workouts/edit-workout.component';
import { ListWorkoutsComponent } from './workout/workouts/list-workouts.component';
import { TableActionsComponent } from './components/table-actions/table-actions.component';
import { EditExerciseComponent } from './exercise/exercises/edit-exercise.component';
import { RPEPipe } from './pipes/rpe-format-pipe';
import { AppErrorHandler } from './app-error-handler';
import { TokenInterceptor } from './token-interceptor';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { WorkoutDetailsEditorComponent } from './workout/workouts/workout-details-editor.component';
import { ExerciseHistoryComponent } from './exercise/exercise-history/exercise-history.component';
import { ViewExerciseComponent } from './exercise/exercises/view-exercise.component';
import { FooterComponent } from './components/footer/footer.component';
import { ToastComponent } from './components/toast/toast.component';
import { ConfirmDeleteComponent } from './components/confirm-delete/confirm-delete.component';
import { ProfileComponent } from './profile/profile/profile.component';
import { OffCanvasComponent } from './components/off-canvas/off-canvas.component';
import { BlockingSpinnerComponent } from './components/blocking-spinner/blocking-spinner.component';
import { NotFoundComponent } from './components/special/not-found.component';
import { RestTimeComponent } from './workout/workouts/rest-time.component';

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
    RPEPipe,
    WorkoutDetailsEditorComponent,
    ExerciseHistoryComponent,
    ViewExerciseComponent,
    FooterComponent,
    ToastComponent,
    ConfirmDeleteComponent,
    ProfileComponent,
    OffCanvasComponent,
    BlockingSpinnerComponent,
    NotFoundComponent,
    RestTimeComponent
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
    NgbModule,
  ],
  providers: [
    { 
      provide: APP_BASE_HREF, 
      useValue: '/app' 
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: environment.useResourceOwnerFlow? TokenInterceptor : AuthHttpInterceptor,
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
