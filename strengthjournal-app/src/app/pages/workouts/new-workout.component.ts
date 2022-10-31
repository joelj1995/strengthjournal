import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { WorkoutService } from 'src/app/services/workout.service';

@Component({
  selector: 'app-new-workout',
  templateUrl: './new-workout.component.html',
  styleUrls: ['./new-workout.component.css']
})
export class NewWorkoutComponent implements OnInit {

  enableSubmit: boolean = true;
  form = new FormGroup({
    title: new FormControl(''),
    date: new FormControl(new Date())
  });

  constructor(private workouts: WorkoutService, private router: Router) { }

  ngOnInit(): void {
    // this.workouts.createWorkoutNow().subscribe(workoutId => {
    //   this.newWorkoutGuid = workoutId;
    //   this.router.navigate(['workouts', 'edit', workoutId]);
    // })
  }

  onSubmit() {

  }

}
