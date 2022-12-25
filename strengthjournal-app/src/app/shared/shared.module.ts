import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableActionsComponent } from './table-actions/table-actions.component';
import { ConfirmDeleteComponent } from './confirm-delete/confirm-delete.component';
import { OffCanvasComponent } from './off-canvas/off-canvas.component';
import { CardComponent } from './card/card.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    TableActionsComponent,
    ConfirmDeleteComponent,
    OffCanvasComponent,
    CardComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  exports: [
    TableActionsComponent,
    ConfirmDeleteComponent,
    OffCanvasComponent,
    CardComponent,
    ReactiveFormsModule
  ]
})
export class SharedModule { }
