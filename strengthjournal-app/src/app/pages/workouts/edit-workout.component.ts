import { Component, OnInit } from '@angular/core';
import { TestBed } from '@angular/core/testing';
import { FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Exercise } from 'src/app/model/exercise';
import { Workout } from 'src/app/model/workout';
import { WorkoutCreateUpdateResult } from 'src/app/model/workout-create-update-result';
import { WorkoutSet } from 'src/app/model/workout-set';
import { ConfigService } from 'src/app/services/config.service';
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

  loadingSets: boolean = false;
  loadingExercises: boolean = true;

  addingSet: boolean = false;
  setBeingUpdated: string | null = null;

  workout: Workout;
  exerciseList: Exercise[] = [];

  dragId: string | null = null;
  dropId: string | null = null;

  showHistory: boolean = false;
  showDetailsEditor: boolean = false;

  lastSetLogged: Date = new Date(Date());

  constructor(
    private route: ActivatedRoute, 
    private workouts: WorkoutService, 
    private exercises : ExerciseService, 
    private config: ConfigService,
    private router: Router) { 
      this.workout = this.route.snapshot.data['workout'];
  }

  static createSetForm(): FormGroup {
    return new FormGroup({
      exerciseId: new FormControl(null, [
        Validators.required
      ]),
      reps: new FormControl(),
      targetReps: new FormControl(),
      weight: new FormControl(),
      rpe: new FormControl(null, [
        Validators.min(0),
        Validators.max(10)
      ])
    })
  }

  sharedSetForm = new FormGroup({
    weightUnit: new FormControl(this.config.getPreferredWeigthUnit())
  });

  updateSetForm = EditWorkoutComponent.createSetForm();

  newSetForm = EditWorkoutComponent.createSetForm();

  ngOnInit(): void {
    this.exercises.getAllExercises().subscribe(page => {
      this.exerciseList = page.data;
      this.loadingExercises = false;
    });
    this.route.paramMap.subscribe(p => {
      this.showHistory = p.get('showHistory') == 'true';
      this.showDetailsEditor = p.get('showDetails') == 'true';
    });
  }

  anyUnsavedChanges(): boolean {
    if (this.addingSet)
      return false;
    if (this.setBeingUpdated != null)
      return true;
    const newSet = this.newSetForm.value;
    return newSet.reps != null || newSet.targetReps != null || newSet.weight != null || newSet.rpe != null;
  }

  logNewSet(doneLogging: boolean = false) {
    const targetForm = this.setBeingUpdated ? this.updateSetForm : this.newSetForm
    const setData = targetForm.value;
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
    this.workouts.syncSet(this.workout.id, newWorkoutSet).subscribe(() => {
      if (this.setBeingUpdated) {
        const indexOfSet = this.workout.sets.findIndex(s => s.id == this.setBeingUpdated);
        this.workout.sets[indexOfSet] = newWorkoutSet;
        this.setBeingUpdated = null;
      }
      else {
        this.lastSetLogged = new Date(Date());
        this.workout.sets.push(newWorkoutSet);
      }
      if (doneLogging) {
        this.router.navigate(['/workouts']);
      } else {
        this.addingSet = false;
      }
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
    this.workouts.deleteSet(this.workout.id, toDelete).subscribe(() => {
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
      });
    }
  }

  moveUp() {
    if (!this.setBeingUpdated)
      throw 'There is no set being updated';
    const i = this.indexOfSetBeingUpdate();
    if (i == 0)
      return;
    const newSets = [...this.workout.sets];
    newSets[i-1] = this.workout.sets[i];
    newSets[i] = this.workout.sets[i-1];
    this.workout.sets = newSets;
    let newSequence = newSets.map(s => s.id);
    this.addingSet = true;
    this.workouts.updateWorkoutSetSequence(this.workout.id, newSequence).subscribe(() => {
      this.addingSet = false;
    });
  }

  moveDown() {
    if (!this.setBeingUpdated)
      throw 'There is no set being updated';
    const i = this.indexOfSetBeingUpdate();
    if (i == this.workout.sets.length - 1)
      return;
    const newSets = [...this.workout.sets];
    newSets[i+1] = this.workout.sets[i];
    newSets[i] = this.workout.sets[i+1];
    this.workout.sets = newSets;
    let newSequence = newSets.map(s => s.id);
    this.addingSet = true;
    this.workouts.updateWorkoutSetSequence(this.workout.id, newSequence).subscribe(() => {
      this.addingSet = false;
    });
  }

  indexOfSetBeingUpdate(): number {
    if (!this.setBeingUpdated)
      throw 'There is no set being updated';
    return this.workout.sets.findIndex(s => s.id == this.setBeingUpdated);
  }

  numberOfSets() {
    return this.workout.sets.length;
  }

  onWorkoutUpdateComplete(workoutData: WorkoutCreateUpdateResult) {
    this.closePopover();
    this.workout.entryDateUTC = workoutData.entryDateUTC;
  }

  differentExerciseSelectedForNewSet() {
    this.newSetForm.setValue({ 
      exerciseId: this.newSetForm.value.exerciseId,
      reps: null,
      targetReps: null,
      weight: null,
      rpe: null
    });
  }

  closePopover() {
    this.router.navigate([{ }], { relativeTo: this.route });
  }

  showHistoryPopover() {
    this.router.navigate([{ showHistory: true }], { relativeTo: this.route });
  }

  showDetailsPopover() {
    this.router.navigate([{ showDetails: true }], { relativeTo: this.route });
  }

  anyErrors(control: string): boolean {
    return this.setBeingUpdated ? !!this.updateSetForm.controls[control].errors : !!this.newSetForm.controls[control].errors
  }

} 
