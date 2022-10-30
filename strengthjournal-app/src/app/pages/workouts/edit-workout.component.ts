import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Exercise } from 'src/app/model/exercise';
import { WorkoutSet } from 'src/app/model/workout-set';
import { ExerciseService } from 'src/app/services/exercise.service';
import { WorkoutService } from 'src/app/services/workout.service';
import { v4 as uuidv4 } from 'uuid';

@Component({
  selector: 'app-edit-workout',
  templateUrl: './edit-workout.component.html',
  styleUrls: ['./edit-workout.component.css']
})
export class EditWorkoutComponent implements OnInit {

  addingSet: boolean = false;

  id: string = '';
  setList: WorkoutSet[] = [];
  exerciseList: Exercise[] = [];
  constructor(private route: ActivatedRoute, private workouts: WorkoutService, private exercises : ExerciseService) { }

  newSetForm = new FormGroup({
    exerciseId: new FormControl(''),
    reps: new FormControl(),
    targetReps: new FormControl(),
    weight: new FormControl(),
    rpe: new FormControl()
  });

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

  logNewSet() {
    this.addingSet = true;
    const setData = this.newSetForm.value;
    const newWorkoutSet: WorkoutSet = {
      id: uuidv4(),
      exerciseId: setData.exerciseId,
      exerciseName: this.exerciseList.find(exercise => exercise.id == setData.exerciseId)?.name ?? '',
      reps: setData.reps,
      targetReps: setData.targetReps,
      weight: setData.weight,
      weightUnit: 'lbs',
      rpe: setData.rpe * 2
    };
    console.log(newWorkoutSet);
    this.workouts.syncSet(this.id, newWorkoutSet).subscribe(() => {
      this.setList.push(newWorkoutSet);
      this.addingSet = false;
    });
  }

}
