import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { ExerciseHistory } from 'src/app/model/exercise-history';
import { ExerciseService } from 'src/app/services/exercise.service';

@Component({
  selector: 'app-exercise-history',
  templateUrl: './exercise-history.component.html',
  styleUrls: ['./exercise-history.component.css']
})
export class ExerciseHistoryComponent implements OnInit {

  historyList: ExerciseHistory[] = [];
  loading: boolean = true;

  @Input()
  exerciseId: string | null = null;

  @Input()
  displayKg: boolean = true;

  constructor(private exercises: ExerciseService) { }

  ngOnInit(): void {
    this.loadExerciseHistory();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['exerciseId']) this.loadExerciseHistory();
  }

  loadExerciseHistory() {
    if (!this.exerciseId) {
      return;
    }
    this.loading = true;
    this.exercises.getExerciseHistory(this.exerciseId).subscribe(historyList => {
      this.loading = false;
      this.historyList = historyList.data;
    })
  }

}
