import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { WorkoutCreateUpdate } from 'src/app/model/workout-create-update';
import { WorkoutService } from 'src/app/services/workout.service';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-workout-details-editor',
  templateUrl: './workout-details-editor.component.html',
  styleUrls: ['./workout-details-editor.component.css']
})
export class WorkoutDetailsEditorComponent implements OnInit {

  @Input()
  workoutId: string | null = null;

  enableSubmit: boolean = true;
  form = new FormGroup({
    title: new FormControl(''),
    date: new FormControl(''),
    time: new FormControl('')
  });

  constructor(private workouts: WorkoutService, private router: Router) { }

  ngOnInit(): void {
  }

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
    const workoutData: WorkoutCreateUpdate = {
      title: this.form.value.title,
      entryDateUTC: this.getDate()
    };
    if (this.workoutId) {
      this.workouts.updateWorkout(this.workoutId, workoutData).subscribe(() => {
        this.enableSubmit = true;
      })
    } else {
      this.workouts.createWorkout(workoutData).subscribe(workoutId => {
        this.router.navigate(['workouts', 'edit', workoutId]);
      });
    }
  }
}
