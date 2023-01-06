import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, combineLatestWith, debounceTime, map, merge, of, startWith, Subject, switchMap, tap } from 'rxjs';
import { ExerciseService } from 'src/app/services/exercise.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-list-exercises',
  templateUrl: './list-exercises.component.html',
  styleUrls: ['./list-exercises.component.css']
})
export class ListExercisesComponent implements OnInit, OnDestroy {

  page: number = 1;
  pageSize: number = 10;
  collectionSize: number = 0;

  stagedDelete: string | null = null;
  loading: boolean = true;

  exerciseSearchInput$ = new Subject<string>();
  activeExerciseSearchInput$ = this.exerciseSearchInput$.pipe(
    tap(() => this.loading = true), 
    debounceTime<string>(1000),
    startWith('')
  );

  pageNumber$ = new BehaviorSubject(this.page);

  exerciseList$ = this.pageNumber$.pipe(
    combineLatestWith(this.activeExerciseSearchInput$),
    switchMap(([pageNumber, search]) => this.getExercisePage(pageNumber, search)),
    tap(() => this.loading = false),
    tap(exercisePage => this.collectionSize = exercisePage.totalRecords),
    map(exercisePage => exercisePage.data)
  );

  constructor(private exercises : ExerciseService, private router: Router) { }

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }

  ngOnInit(): void {
  }

  deleteExercise(exerciseId: string) {
    this.stagedDelete = exerciseId;
  }

  dismissDeleteExercise() {
    this.stagedDelete = null;
  }

  confirmDeleteExercise() {
    if (this.stagedDelete == null) throw 'Tried to finalize delete but no record it staged';
    this.subs.sink = this.exercises.deleteExercise(this.stagedDelete).subscribe(() => {
      this.stagedDelete = null;
      this.pageNumber$.next(this.page);
    });
  }

  editExercise(exerciseId: string) {
    this.router.navigate(['exercises', exerciseId, 'edit']);
  }

  nextPage() {
    this.pageNumber$.next(this.page);
  }

  getExercisePage(page: number, exerciseSearch: string) {
    return this.exercises.getExercises(page, this.pageSize, exerciseSearch);
  }

  searchInputChange(e: any) {
    this.exerciseSearchInput$.next(e.target.value);
  }

  private subs = new SubSink();

}
