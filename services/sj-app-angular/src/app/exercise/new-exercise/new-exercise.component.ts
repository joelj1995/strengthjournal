import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Exercise } from 'src/app/model/exercise';
import { ExerciseService } from 'src/app/services/exercise.service';
import { ToastService } from 'src/app/services/toast.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-new-exercise',
  templateUrl: './new-exercise.component.html',
  styleUrls: ['./new-exercise.component.css']
})
export class NewExerciseComponent implements OnInit, OnDestroy {

  enableSubmit: boolean = true;
  exerciseList: Exercise[] | null = null;

  form = new FormGroup({
    name: new FormControl(''),
    parentExerciseId: new FormControl(null)
  });

  constructor(
    private exercises : ExerciseService, 
    private router: Router, 
    private toast: ToastService) { }

  ngOnDestroy(): void {
    throw new Error('Method not implemented.');
  }

  ngOnInit(): void {
    this.subs.sink = this.exercises.getAllExercises().subscribe(page => {
      this.exerciseList = page.data.filter(e => e.parentExerciseId == null);
    });
  }

  onSubmit() {
    this.enableSubmit = false;
    this.subs.sink = this.exercises.createExercise(this.form.value.name, this.form.value.parentExerciseId).subscribe(e => {
      this.toast.setToast({ message: 'Exercise created', domClass: 'bg-success text-light' });
      this.router.navigate(['exercises']);
    });
  }

  private subs = new SubSink();

}
