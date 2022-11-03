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

  systemExerciseList: Exercise[] | null = null;

  constructor(private exercises : ExerciseService, private router: Router) { }

  ngOnInit(): void {
    this.exercises.getExercises().subscribe(exercises => {
      this.systemExerciseList = exercises;
    })
  }

  deleteExercise(exerciseId: string) {
    this.exercises.deleteExercise(exerciseId).subscribe(() => {
      this.systemExerciseList = this.systemExerciseList?.filter(e => e.id !== exerciseId) ?? null;
    })
  }

  editExercise(exerciseId: string) {
    this.router.navigate(['exercises', 'edit', exerciseId]);
  }
}
