import { Component, Input, OnInit } from '@angular/core';
import { ExerciseHistory } from 'src/app/model/exercise-history';
import { ExerciseService } from 'src/app/services/exercise.service';

@Component({
  selector: 'app-exercise-history-view',
  templateUrl: './exercise-history-view.component.html',
  styleUrls: ['./exercise-history-view.component.css']
})
export class ExerciseHistoryViewComponent implements OnInit {

  historyList: ExerciseHistory[] = [];
  loading: boolean = true;

  @Input()
  exerciseId: string | null = null;

  constructor(private exercises: ExerciseService) { }

  ngOnInit(): void {
    this.loadExerciseHistory();
  }

  ngOnChanges(): void {
    this.loadExerciseHistory();
  }

  loadExerciseHistory() {
    if (!this.exerciseId) {
      return;
    }
    this.loading = true;
    this.exercises.getExerciseHistory(this.exerciseId).subscribe(historyList => {
      this.loading = false;
      this.historyList = historyList;
    })
  }

}
