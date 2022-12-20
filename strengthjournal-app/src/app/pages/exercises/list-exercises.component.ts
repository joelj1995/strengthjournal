import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, debounceTime, map, Subject, switchMap, tap } from 'rxjs';
import { Exercise } from 'src/app/model/exercise';
import { ExerciseService } from 'src/app/services/exercise.service';

@Component({
  selector: 'app-list-exercises',
  templateUrl: './list-exercises.component.html',
  styleUrls: ['./list-exercises.component.css']
})
export class ListExercisesComponent implements OnInit {

  page: number = 1;
  pageSize: number = 10;
  collectionSize: number = 0;

  stagedDelete: string | null = null;
  loading: boolean = false;

  exerciseSearchInput = new BehaviorSubject('');
  exerciseSearch: string = '';

  // TODO: subscribe to page event
  exerciseList$ = this.exerciseSearchInput.pipe(
    tap(() => this.loading = true),
    debounceTime(1000),
    tap(search => this.exerciseSearch = search as string),
    switchMap(search => this.getExercisePage()),
    tap(() => this.loading = false),
    tap(exercisePage => this.collectionSize = exercisePage.totalRecords),
    map(exercisePage => exercisePage.data)
  );

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
      this.stagedDelete = null;
      this.exerciseSearchInput.next(this.exerciseSearch);
    })
  }

  editExercise(exerciseId: string) {
    this.router.navigate(['exercises', exerciseId, 'edit']);
  }

  nextPage() {
    this.exerciseSearchInput.next(this.exerciseSearch);
  }

  getExercisePage() {
    return this.exercises.getExercises(this.page, this.pageSize, this.exerciseSearch);
  }

  searchInputChange(e: any) {
    this.exerciseSearchInput.next(e.target.value);
  }

}
