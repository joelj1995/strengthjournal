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
  pageSize: number = 10;
  collectionSize: number = 0;

  workoutList: Workout[] | null = null;

  stagedDelete: string | null = null;

  constructor(private workouts: WorkoutService, private router: Router) { }

  getWorkoutPage() {
    this.workouts.getWorkouts(this.page, this.pageSize).subscribe(page => {
      this.workoutList = page.data;
      this.collectionSize = page.totalRecords;
    });
  }

  ngOnInit(): void {
    this.getWorkoutPage();
  }

  deleteWorkout(workoutId: string) {
    this.stagedDelete = workoutId;
  }

  dismissDeleteWorkout() {
    this.stagedDelete = null;
  }

  confirmDeleteWorkout() {
    if (this.stagedDelete == null) throw "Tried to finalize workout delete but no ID staged";
    this.workouts.deleteWorkout(this.stagedDelete).subscribe(() => {
      this.workoutList = this.workoutList?.filter(w => w.id != this.stagedDelete) ?? null;
      this.getWorkoutPage();
      this.stagedDelete = null;
    });
  }

  editWorkout(workoutId: string) {
    this.router.navigate(['workouts', workoutId, 'edit']);
  }

}
