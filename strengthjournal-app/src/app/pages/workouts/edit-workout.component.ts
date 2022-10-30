import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Exercise } from 'src/app/model/exercise';
import { WorkoutSet } from 'src/app/model/workout-set';
import { ExerciseService } from 'src/app/services/exercise.service';
import { WorkoutService } from 'src/app/services/workout.service';

@Component({
  selector: 'app-edit-workout',
  templateUrl: './edit-workout.component.html',
  styleUrls: ['./edit-workout.component.css']
})
export class EditWorkoutComponent implements OnInit {

  id: string = '';
  setList: WorkoutSet[] = [];
  exerciseList: Exercise[] = [];
  constructor(private route: ActivatedRoute, private workouts: WorkoutService, private exercises : ExerciseService) { }

  ngOnInit(): void {
    this.exercises.getExercises().subscribe(exercises => {
      this.exerciseList = exercises;
    });
    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.setList = [];
      this.workouts.getWorkoutSets(this.id).subscribe(sets => {
        this.setList = sets;
      })
    });
  }

}
