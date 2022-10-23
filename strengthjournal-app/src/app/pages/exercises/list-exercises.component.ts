import { Component, OnInit } from '@angular/core';
import { Exercise } from 'src/app/model/exercise';
import { ExerciseService } from 'src/app/services/exercise.service';

@Component({
  selector: 'app-list-exercises',
  templateUrl: './list-exercises.component.html',
  styleUrls: ['./list-exercises.component.css']
})
export class ListExercisesComponent implements OnInit {

  systemExerciseList: Exercise[] | null = null;

  constructor(private exercises : ExerciseService) { }

  ngOnInit(): void {
    this.exercises.getExercises().subscribe(exercises => {
      this.systemExerciseList = exercises;
    })
  }

}
