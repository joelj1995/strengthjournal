import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ExerciseService } from 'src/app/services/exercise.service';

@Component({
  selector: 'app-new-exercise',
  templateUrl: './new-exercise.component.html',
  styleUrls: ['./new-exercise.component.css']
})
export class NewExerciseComponent implements OnInit {

  enableSubmit: boolean = true;

  form = new FormGroup({
    name: new FormControl('')
  });

  constructor(private exercises : ExerciseService) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.enableSubmit = false;
    console.log(this.form.value.name);
    this.exercises.createExercise(this.form.value.name).subscribe(e => {
      this.enableSubmit = true;
    });
  }

}
