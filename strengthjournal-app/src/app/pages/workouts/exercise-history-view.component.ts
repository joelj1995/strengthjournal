import { Component, Input, OnInit } from '@angular/core';
import { ExerciseService } from 'src/app/services/exercise.service';

@Component({
  selector: 'app-exercise-history-view',
  templateUrl: './exercise-history-view.component.html',
  styleUrls: ['./exercise-history-view.component.css']
})
export class ExerciseHistoryViewComponent implements OnInit {

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
    this.loading = true;
    if (!this.exerciseId) {
      return;
    }
    this.exercises.getExerciseHistory(this.exerciseId).subscribe(historyList => {
      console.log(historyList);
    })
  }

}
