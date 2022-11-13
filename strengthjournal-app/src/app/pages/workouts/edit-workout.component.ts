import { Component, OnInit } from '@angular/core';
import { TestBed } from '@angular/core/testing';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Exercise } from 'src/app/model/exercise';
import { Workout } from 'src/app/model/workout';
import { WorkoutSet } from 'src/app/model/workout-set';
import { ExerciseService } from 'src/app/services/exercise.service';
import { ToastService } from 'src/app/services/toast.service';
import { WorkoutService } from 'src/app/services/workout.service';
import { v4 as uuidv4 } from 'uuid';
// import { RPEPipe } from 'src/app/pipes/rpe-format-pipe';

@Component({
  selector: 'app-edit-workout',
  templateUrl: './edit-workout.component.html',
  styleUrls: ['./edit-workout.component.css']
})
export class EditWorkoutComponent implements OnInit {

  loadingSets: boolean = true;
  loadingExercises: boolean = true;

  addingSet: boolean = false;
  setBeingUpdated: string | null = null;

  id: string = '';
  workout: Workout = {
    id: '',
    title: '',
    entryDateUTC: new Date(),
    sets: [],
    bodyweight: null,
    bodyweightUnit: ''
  };
  exerciseList: Exercise[] = [];

  dragId: string | null = null;
  dropId: string | null = null;

  constructor(private route: ActivatedRoute, private workouts: WorkoutService, private exercises : ExerciseService, private toast: ToastService) { }

  sharedSetForm = new FormGroup({
    weightUnit: new FormControl('lbs')
  });

  updateSetForm = new FormGroup({
    exerciseId: new FormControl(''),
    reps: new FormControl(),
    targetReps: new FormControl(),
    weight: new FormControl(),
    rpe: new FormControl()
  });

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
      this.loadingExercises = false;
    });
    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.workouts.getWorkout(this.id).subscribe(workout => {
        this.workout = workout;
        this.loadingSets = false;
      })
    });
  }

  logNewSet() {
    const setData = this.setBeingUpdated ? this.updateSetForm.value : this.newSetForm.value;
    if (!setData.exerciseId) {
      this.toast.setToast({ message: 'Exercise name is requried', domClass: 'bg-danger text-light' });
      return;
    }
    const newWorkoutSet: WorkoutSet = {
      id: this.setBeingUpdated ?? uuidv4(),
      exerciseId: setData.exerciseId,
      exerciseName: this.exerciseList.find(exercise => exercise.id == setData.exerciseId)?.name ?? '',
      reps: setData.reps,
      targetReps: setData.targetReps,
      weight: setData.weight,
      weightUnit: this.sharedSetForm.value.weightUnit,
      rpe: setData.rpe * 2
    };
    this.addingSet = true;
    this.workouts.syncSet(this.id, newWorkoutSet).subscribe(() => {
      if (this.setBeingUpdated) {
        const indexOfSet = this.workout.sets.findIndex(s => s.id == this.setBeingUpdated);
        this.workout.sets[indexOfSet] = newWorkoutSet;
        this.setBeingUpdated = null;
      }
      else {
        this.workout.sets.push(newWorkoutSet);
      }
      this.addingSet = false;
    });
  }

  startUpdatingSet(setId: string) {
    if (this.setBeingUpdated == setId) {
      return;
    }
    const setData = this.workout.sets.find(s => s.id == setId);
    this.updateSetForm.setValue({ 
      exerciseId: setData?.exerciseId,
      reps: setData?.reps,
      targetReps: setData?.targetReps,
      weight: setData?.weight,
      rpe: setData?.rpe ? setData?.rpe / 2 : null
    });
    this.sharedSetForm.setValue({ weightUnit: setData?.weightUnit })
    this.setBeingUpdated = setId;
  }

  stopUpdatingSet() {
    this.setBeingUpdated = null;
  }

  deleteSet() {
    this.addingSet = true;
    if (!this.setBeingUpdated) {
      throw 'Tried deleting set but none selected'
    }
    const toDelete = this.setBeingUpdated;
    this.workouts.deleteSet(this.id, toDelete).subscribe(() => {
      this.setBeingUpdated = null;
      this.workout.sets = this.workout.sets.filter(s => s.id != toDelete);
      this.addingSet = false;
    })
  }

  rowDragStart(ev: any, setId: string) {
    this.dragId = setId;
    ev.dataTransfer.setData("application/my-app", setId);
    ev.dataTransfer.dropEffect = 'move';
  }

  rowDragEnd(ev: any, setId: string) {
    this.dragId = null;
    this.dropId = null;
  }

  rowDragOver(ev: any, setId: string) {
    ev.preventDefault();
    this.dropId = setId;
  }

  rowDrop(ev: any, setId: string) {
    if (this.dragId != null && this.dropId != null && this.dragId != this.dropId) {
      let newSets = [];
      const dragIdx = this.workout.sets.findIndex(s => s.id == this.dragId);
      const dropIdx = this.workout.sets.findIndex(s => s.id == this.dropId);
      for (let i = 0; i < this.workout.sets.length; i++) {
        if ((i < dragIdx && i < dropIdx) || (i > dragIdx && i > dropIdx)) {
          newSets.push(this.workout.sets[i]);
        }
        else if (i == dropIdx) {
          newSets.push(this.workout.sets[dragIdx]);
        }
        else {
          if (dragIdx < dropIdx) {
            newSets.push(this.workout.sets[i+1]);
          } else {
            newSets.push(this.workout.sets[i-1]);
          }
        }
      }
      this.workout.sets = newSets;
      let newSequence = newSets.map(s => s.id);
      this.addingSet = true;
      this.workouts.updateWorkoutSetSequence(this.workout.id, newSequence).subscribe(() => {
        this.addingSet = false;
      })
    }
  }

}
