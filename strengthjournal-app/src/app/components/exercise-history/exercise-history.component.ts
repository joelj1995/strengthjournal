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
  loadingMore = false;
  noMoreRecords = false;

  pageNumber = 1;
  readonly perPage = 10;

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
    this.noMoreRecords = false;
    this.loadingMore = false;
    this.pageNumber = 1;
    this.loading = true;
    this.exercises.getExerciseHistory(this.exerciseId, this.pageNumber, this.perPage).subscribe(historyList => {
      this.loading = false;
      if (historyList.totalRecords <= this.pageNumber * this.perPage)
        this.noMoreRecords = true;
      this.historyList = historyList.data;
    });
  }

  loadMore() {
    if (!this.exerciseId) {
      return;
    }
    this.loadingMore = true;
    this.pageNumber += 1;
    this.exercises.getExerciseHistory(this.exerciseId, this.pageNumber, this.perPage).subscribe(historyList => {
      this.loadingMore = false;
      if (historyList.totalRecords <= this.pageNumber * this.perPage)
        this.noMoreRecords = true;
      this.historyList = this.historyList.concat(historyList.data);
    });
  }

}
