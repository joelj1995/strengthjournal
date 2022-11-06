import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { WorkoutCreate } from 'src/app/model/workout-create';
import { WorkoutService } from 'src/app/services/workout.service';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-new-workout',
  templateUrl: './new-workout.component.html',
  styleUrls: ['./new-workout.component.css']
})
export class NewWorkoutComponent implements OnInit {

  enableSubmit: boolean = true;
  form = new FormGroup({
    title: new FormControl(''),
    date: new FormControl(''),
    time: new FormControl('')
  });

  constructor(private workouts: WorkoutService, private router: Router) { }

  ngOnInit(): void { }

  getDate(): Date {
    const date = this.form.value.date;
    const time = this.form.value.time;
    let dateString = `${date.year}-${date.month}-${date.day}`;
    if (time) {
      dateString += ` ${time.hour}:${time.minute}`;
    }
    return new Date(dateString);
  }

  onSubmit() {
    this.enableSubmit = false;
    const workoutData: WorkoutCreate = {
      title: this.form.value.title,
      entryDateUTC: this.getDate()
    };
    console.log(workoutData);
    this.workouts.createWorkout(workoutData).subscribe(workoutId => {
      this.router.navigate(['workouts', 'edit', workoutId]);
    });
  }

}
