import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { Workout } from 'src/app/model/workout';
import { WorkoutService } from 'src/app/services/workout.service';

@Component({
  selector: 'app-list-workouts',
  templateUrl: './list-workouts.component.html',
  styleUrls: ['./list-workouts.component.css']
})
export class ListWorkoutsComponent implements OnInit {

  page: number = 1;
  pageSize: number = 5;
  collectionSize: number = 0;

  workoutList: Workout[] | null = null;

  constructor(private workouts: WorkoutService, private router: Router) { }

  getWorkoutPage() {
    this.workouts.getWorkouts(this.page, this.pageSize).subscribe(page => {
      this.workoutList = page.workouts;
      this.collectionSize = page.totalRecords;
    });
  }

  ngOnInit(): void {
    this.getWorkoutPage();
  }

  deleteWorkout(workoutId: string) {
    this.workouts.deleteWorkout(workoutId).subscribe(() => {
      this.workoutList = this.workoutList?.filter(w => w.id != workoutId) ?? null;
      this.getWorkoutPage();
    })
  }

  editWorkout(workoutId: string) {
    this.router.navigate(['workouts', 'edit', workoutId]);
  }

}
