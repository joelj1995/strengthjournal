import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Workout } from 'src/app/model/workout';
import { WorkoutService } from 'src/app/services/workout.service';

@Component({
  selector: 'app-list-workouts',
  templateUrl: './list-workouts.component.html',
  styleUrls: ['./list-workouts.component.css']
})
export class ListWorkoutsComponent implements OnInit {

  workoutList: Workout[] | null = null;

  constructor(private workouts: WorkoutService, private router: Router) { }

  ngOnInit(): void {
    this.workouts.getWorkouts(1, 5).subscribe(page => {
      this.workoutList = page.workouts;
    })
  }

  deleteWorkout(workoutId: string) {
    this.workouts.deleteWorkout(workoutId).subscribe(() => {
      this.workoutList = this.workoutList?.filter(w => w.id != workoutId) ?? null;
    })
  }

  editWorkout(workoutId: string) {
    this.router.navigate(['workouts', 'edit', workoutId]);
  }

}
