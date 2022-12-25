import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableActionsComponent } from './table-actions/table-actions.component';
import { ConfirmDeleteComponent } from './confirm-delete/confirm-delete.component';
import { OffCanvasComponent } from './off-canvas/off-canvas.component';
import { CardComponent } from './card/card.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ExerciseHistoryComponent } from './exercise-history/exercise-history.component';
import { RPEPipe } from './rpe-format-pipe';

@NgModule({
  declarations: [
    TableActionsComponent,
    ConfirmDeleteComponent,
    OffCanvasComponent,
    CardComponent,
    ExerciseHistoryComponent,
    RPEPipe
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    NgbModule
  ],
  exports: [
    TableActionsComponent,
    ConfirmDeleteComponent,
    OffCanvasComponent,
    CardComponent,
    ReactiveFormsModule,
    NgbModule,
    ExerciseHistoryComponent,
    RPEPipe
  ]
})
export class SharedModule { }
