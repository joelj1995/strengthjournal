import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { WorkoutCreate } from 'src/app/model/workout-create';
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

  ngOnInit(): void { }

  onSubmit() {
    this.enableSubmit = false;
    const workoutData: WorkoutCreate = {
      title: this.form.value.title,
      entryDateUTC: this.form.value.date
    };
    this.workouts.createWorkout(workoutData).subscribe(workoutId => {
      this.router.navigate(['workouts', 'edit', workoutId]);
    });
  }

}
