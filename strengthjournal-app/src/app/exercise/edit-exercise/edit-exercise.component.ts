import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Exercise } from 'src/app/model/exercise';
import { ExerciseService } from 'src/app/services/exercise.service';
import { ToastService } from 'src/app/services/toast.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-edit-exercise',
  templateUrl: './edit-exercise.component.html',
  styleUrls: ['./edit-exercise.component.css']
})
export class EditExerciseComponent implements OnInit, OnDestroy {

  loadingData: boolean = false;
  enableSubmit: boolean = true;
  id: string = '';
  exerciseList: Exercise[] | null = null;

  form = new FormGroup({
    name: new FormControl(''),
    parentExerciseId: new FormControl(null)
  });

  constructor(private exercises: ExerciseService, private router: Router, private route: ActivatedRoute, private toast: ToastService) { 
    const resolvedEditExercise = this.route.snapshot.data['resolvedEditExercise'];
    const exercise = resolvedEditExercise.exercise;
    this.form.setValue({ 'name': exercise?.name, 'parentExerciseId': exercise?.parentExerciseId });
    this.exerciseList = resolvedEditExercise.exerciseList;
    this.id = exercise.id;
  }

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }

  ngOnInit(): void {

  }

  onSubmit() {
    this.enableSubmit = false;
    this.subs.sink = this.exercises.updateExercise(this.id, this.form.value.name, this.form.value.parentExerciseId).subscribe(e => {
      this.toast.setToast({ message: 'Exercise updated', domClass: 'bg-success text-light' })
      this.enableSubmit = true;
      this.router.navigate(['exercises']);
    });
  }

  private subs = new SubSink();

}
