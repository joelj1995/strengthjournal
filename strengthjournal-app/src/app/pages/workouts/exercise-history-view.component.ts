import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-exercise-history-view',
  templateUrl: './exercise-history-view.component.html',
  styleUrls: ['./exercise-history-view.component.css']
})
export class ExerciseHistoryViewComponent implements OnInit {

  @Input()
  exerciseId: string | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
