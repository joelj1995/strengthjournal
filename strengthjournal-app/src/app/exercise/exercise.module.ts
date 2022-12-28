import { NgModule } from '@angular/core';
import { NewExerciseComponent } from './new-exercise/new-exercise.component';
import { ListExercisesComponent } from './list-exercises/list-exercises.component';
import { EditExerciseComponent } from './edit-exercise/edit-exercise.component';
import { ViewExerciseComponent } from './view-exercise/view-exercise.component';
import { RouterModule, Routes } from '@angular/router';
import { EditExerciseResolver } from './edit-exercise.resolver';
import { ExerciseResolver } from './exercise.resolver';
import { SharedModule } from '../shared/shared.module';

const routes: Routes = [
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
];

@NgModule({
  declarations: [
    ListExercisesComponent,
    NewExerciseComponent,
    EditExerciseComponent,
    ViewExerciseComponent
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ]
})
export class ExerciseModule { }
