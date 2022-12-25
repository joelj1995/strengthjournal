import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { map, Observable, tap } from 'rxjs';
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

  workoutList$: Observable<Workout[]> | undefined;

  stagedDelete: string | null = null;

  constructor(private workouts: WorkoutService, private router: Router) { }

  getWorkoutPage() {
    this.workoutList$ = this.workouts.getWorkouts(this.page, this.pageSize).pipe(
      tap(page => this.collectionSize = page.totalRecords),
      map(page => page.data)
    );
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
      this.getWorkoutPage();
      this.stagedDelete = null;
    });
  }

  editWorkout(workoutId: string) {
    this.router.navigate(['workouts', workoutId, 'edit']);
  }

}
