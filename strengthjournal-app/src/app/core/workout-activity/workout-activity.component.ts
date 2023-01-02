import { Component, OnDestroy, OnInit } from '@angular/core';
import { WorkoutActivity } from 'src/app/model/workout-activity';
import { WorkoutService } from 'src/app/services/workout.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-workout-activity',
  templateUrl: './workout-activity.component.html',
  styleUrls: ['./workout-activity.component.css']
})
export class WorkoutActivityComponent implements OnInit, OnDestroy {

  workoutActivities: WorkoutActivity[] | undefined;

  constructor(private workout: WorkoutService) { }

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }

  ngOnInit(): void {
    this.subs.sink = this.workout.getWorkoutActivity(0, 5)
      .subscribe(activityData => {
        this.workoutActivities = activityData;
      });
  }

  private subs = new SubSink();

}
