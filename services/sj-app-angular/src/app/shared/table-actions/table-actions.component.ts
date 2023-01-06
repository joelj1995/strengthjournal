import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-table-actions',
  templateUrl: './table-actions.component.html',
  styleUrls: ['./table-actions.component.css']
})
export class TableActionsComponent implements OnInit {

  @Output() editClicked: EventEmitter<any> = new EventEmitter();
  @Output() deleteClicked: EventEmitter<any> = new EventEmitter();

  constructor() { }

  ngOnInit(): void {
  }

}
