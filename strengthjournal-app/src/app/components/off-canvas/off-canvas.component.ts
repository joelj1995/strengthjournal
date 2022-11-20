import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-off-canvas',
  templateUrl: './off-canvas.component.html',
  styleUrls: ['./off-canvas.component.css']
})
export class OffCanvasComponent implements OnInit {

  @Input()
  title: string = '';

  @Input()
  show: boolean = false;

  @Output()
  public closeEvent = new EventEmitter<undefined>();

  constructor() { }

  ngOnInit(): void {
  }

}
