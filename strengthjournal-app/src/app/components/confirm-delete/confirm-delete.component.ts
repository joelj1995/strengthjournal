import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-confirm-delete',
  templateUrl: './confirm-delete.component.html',
  styleUrls: ['./confirm-delete.component.css']
})
export class ConfirmDeleteComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  @Output() 
  public dismissDeleteEvent = new EventEmitter<undefined>();

  @Output() 
  public confirmDeleteEvent = new EventEmitter<undefined>();

}
