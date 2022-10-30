import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
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

  constructor(private exercises : ExerciseService, private router: Router) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.enableSubmit = false;
    console.log(this.form.value.name);
    this.exercises.createExercise(this.form.value.name).subscribe(e => {
      this.router.navigate(['exercises']);
    });
  }

}
