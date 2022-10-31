import { Component, OnInit } from '@angular/core';
import { Workout } from 'src/app/model/workout';
import { WorkoutService } from 'src/app/services/workout.service';

@Component({
  selector: 'app-list-workouts',
  templateUrl: './list-workouts.component.html',
  styleUrls: ['./list-workouts.component.css']
})
export class ListWorkoutsComponent implements OnInit {

  workoutList: Workout[] | null = null;

  constructor(private workouts: WorkoutService) { }

  ngOnInit(): void {
    this.workouts.getWorkouts().subscribe(workouts => {
      this.workoutList = workouts;
    })
  }

}
