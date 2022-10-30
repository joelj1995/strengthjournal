import { Component, OnInit } from '@angular/core';
import { WorkoutService } from 'src/app/services/workout.service';

@Component({
  selector: 'app-new-workout',
  templateUrl: './new-workout.component.html',
  styleUrls: ['./new-workout.component.css']
})
export class NewWorkoutComponent implements OnInit {

  failed: boolean = false;
  newWorkoutGuid: string | null = null;

  constructor(private workouts: WorkoutService) { }

  ngOnInit(): void {
    this.workouts.createWorkoutNow().subscribe(workoutId => {
      this.newWorkoutGuid = workoutId;
    })
  }

}
