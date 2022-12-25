import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableActionsComponent } from './table-actions/table-actions.component';
import { ConfirmDeleteComponent } from './confirm-delete/confirm-delete.component';
import { OffCanvasComponent } from './off-canvas/off-canvas.component';

@NgModule({
  declarations: [
    TableActionsComponent,
    ConfirmDeleteComponent,
    OffCanvasComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    TableActionsComponent,
    ConfirmDeleteComponent,
    OffCanvasComponent
  ]
})
export class SharedModule { }
