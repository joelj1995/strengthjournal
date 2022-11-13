import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Exercise } from 'src/app/model/exercise';
import { ExerciseService } from 'src/app/services/exercise.service';

@Component({
  selector: 'app-list-exercises',
  templateUrl: './list-exercises.component.html',
  styleUrls: ['./list-exercises.component.css']
})
export class ListExercisesComponent implements OnInit {

  page: number = 1;
  pageSize: number = 5;
  collectionSize: number = 0;

  exerciseList: Exercise[] | null = null;

  stagedDelete: string | null = null;

  constructor(private exercises : ExerciseService, private router: Router) { }

  ngOnInit(): void {
    this.getExercisePage();
  }

  deleteExercise(exerciseId: string) {
    this.stagedDelete = exerciseId;
  }

  dismissDeleteExercise() {
    this.stagedDelete = null;
  }

  confirmDeleteExercise() {
    if (this.stagedDelete == null) throw 'Tried to finalize delete but no record it staged';
    this.exercises.deleteExercise(this.stagedDelete).subscribe(() => {
      this.exerciseList = this.exerciseList?.filter(e => e.id !== this.stagedDelete) ?? null;
      this.stagedDelete = null;
      this.getExercisePage();
    })
  }

  editExercise(exerciseId: string) {
    this.router.navigate(['exercises', 'edit', exerciseId]);
  }

  getExercisePage() {
    this.exercises.getExercises(this.page, this.pageSize).subscribe(page => {
      this.exerciseList = page.data;
      this.collectionSize = page.totalRecords;
    });
  }
}
