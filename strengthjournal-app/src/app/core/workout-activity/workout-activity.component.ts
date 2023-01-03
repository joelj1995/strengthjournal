import { Component, OnDestroy, OnInit } from '@angular/core';
import { BehaviorSubject, map, pipe, scan, switchMap, tap, withLatestFrom } from 'rxjs';
import { WorkoutActivity } from 'src/app/model/workout-activity';
import { WorkoutService } from 'src/app/services/workout.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-workout-activity',
  templateUrl: './workout-activity.component.html',
  styleUrls: ['./workout-activity.component.css']
})
export class WorkoutActivityComponent implements OnInit, OnDestroy {

  loadingMore: boolean = false;
  noMoreRecords: boolean = false;

  pageNumber$ = new BehaviorSubject(1);
  readonly perPage = 5;

  workoutActivities: WorkoutActivity[] | undefined;

  constructor(private workout: WorkoutService) { }

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }

  ngOnInit(): void {
    this.subs.sink = this.pageNumber$
      .pipe(
        switchMap(page => this.workout.getWorkoutActivity(page, this.perPage)),
        tap(page => this.noMoreRecords = page.totalRecords <= this.pageNumber$.getValue() * this.perPage),
        map(page => page.data),
        scan((acc, value) => [...acc, ...value])
      )
      .subscribe(data => {
        this.workoutActivities = data;
        this.loadingMore = false;
      });
  }

  loadMore() {
    this.loadingMore = true;
    const currentPage = this.pageNumber$.getValue();
    this.pageNumber$.next(currentPage + 1);
  }

  private subs = new SubSink();

}
