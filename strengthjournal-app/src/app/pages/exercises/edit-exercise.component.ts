import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ExerciseService } from 'src/app/services/exercise.service';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'app-edit-exercise',
  templateUrl: './edit-exercise.component.html',
  styleUrls: ['./edit-exercise.component.css']
})
export class EditExerciseComponent implements OnInit {

  loadingData: boolean = true;
  enableSubmit: boolean = true;
  id: string = '';

  form = new FormGroup({
    name: new FormControl('')
  });

  constructor(private exercises: ExerciseService, private router: Router, private route: ActivatedRoute, private toast: ToastService) { }

  ngOnInit(): void {
    this.enableSubmit = false;
    this.loadingData = true;
    this.route.params.subscribe(params => {
      this.enableSubmit = true;
      this.id = params['id'];
      this.exercises.getAllExercises().subscribe(page => {
        let exercise = page.data.find(e => e.id == this.id);
        this.form.setValue({'name': exercise?.name});
        this.loadingData = false;
      })
    });
  }

  onSubmit() {
    this.enableSubmit = false;
    this.exercises.updateExercise(this.id, this.form.value.name).subscribe(e => {
      this.toast.setToast({ message: 'Exercise updated', domClass: 'bg-success text-light' })
      this.enableSubmit = true;
      this.router.navigate(['exercises']);
    });
  }

}
