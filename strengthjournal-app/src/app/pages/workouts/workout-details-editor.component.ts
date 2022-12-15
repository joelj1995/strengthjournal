import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { WorkoutService } from 'src/app/services/workout.service';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { ToastService } from 'src/app/services/toast.service';
import { ConfigService } from 'src/app/services/config.service';
import { WorkoutCreateUpdateResult } from 'src/app/model/workout-create-update-result';
import { WorkoutUpdate } from 'src/app/model/workout-update';

@Component({
  selector: 'app-workout-details-editor',
  templateUrl: './workout-details-editor.component.html',
  styleUrls: ['./workout-details-editor.component.css']
})
export class WorkoutDetailsEditorComponent implements OnInit {

  @Input()
  workoutId: string | null = null;

  @Input()
  workoutData: WorkoutUpdate | null = null;

  @Output()
  public updateComplete = new EventEmitter<WorkoutCreateUpdateResult>();

  pickerDate: NgbDateStruct = { year: 1789, month: 7, day: 14 };
  pickerTime = {hour: 0, minute: 0};

  validationErrors: {[key: string]: {[key: string]: string}} = {
    'title': { 'maxlength': 'Title cannot exceed 255 characters' },
    'date': { 'required': 'Please enter a valid date with format yyyy-mm-dd' },
    'bodyweight': {
      'min': 'Bodyweight cannot be negative',
      'max': 'Bodyweight cannot be more than 1000'
    },
    'notes': {
      'maxlength': 'Your notes are too long'
    }
  };

  enableSubmit: boolean = true;
  form!: FormGroup;

  constructor(
    private workouts: WorkoutService, 
    private toast: ToastService,
    private fb: FormBuilder) { 
      this.form = fb.group({
        title: ['', [Validators.maxLength(255)]],
        date: ['', [Validators.required]],
        time: [''],
        bodyweight: ['', [Validators.min(0), Validators.max(1000)]],
        bodyweightUnit: [''],
        notes: ['', [Validators.maxLength(2048)]]
      });
  }

  getErrors(fc: string): string | null {
    if (this.form.get(fc)?.errors ?? 0 > 0) {
      const firstError = Object.keys(this.form.get(fc)?.errors as Object)[0] as string;
      try {
        return this.validationErrors[fc][firstError] as string;
      }
      catch (e) {
        console.error('Error getting validation string: ' + e);
        return 'Error validating this field';
      }
    }
    return null;
  }

  bindInputDateToPicker() {
    if (this.workoutData == null)
      throw 'Cannot bind date to null workout';
    const date = new Date(this.workoutData.entryDateUTC);
    const datePickerValue = { year: date.getFullYear(), month: date.getMonth() + 1, day: date.getDate() };
    this.pickerDate = datePickerValue;
    const timePickerValue = { hour: date.getHours(), minute: date.getMinutes() };
    this.pickerTime = timePickerValue;
  }

  bindFormToWorkout() {
    if (this.workoutData == null)
      throw 'Cannot bind form to null workout';
    this.form.setValue({
      title: this.workoutData.title,
      date: '',
      time: '',
      bodyweight: this.workoutData.bodyweight,
      bodyweightUnit: this.workoutData.bodyweightUnit,
      notes: this.workoutData.notes
    });
  }

  ngOnInit(): void {
    if (this.workoutData == null)
      return;
    this.bindFormToWorkout();
    this.bindInputDateToPicker();
  }

  ngOnChanges() {
    if (this.workoutData == null)
      return;
    this.bindFormToWorkout();
    this.bindInputDateToPicker();
  }

  getDate(forceUtc: boolean = true): Date {
    const date = this.form.value.date;
    const time = this.form.value.time;
    let dateString = `${date.year}-${date.month}-${date.day}`;
    if (time) {
      dateString += ` ${time.hour}:${time.minute}`;
    }
    if (forceUtc)
      dateString += ' UTC';
    const returnDate = new Date(dateString);
    return returnDate;
  }

  onSubmit() {
    this.enableSubmit = false;
    const localDate = this.getDate(false);
    const workoutData: WorkoutUpdate = {
      title: this.form.value.title,
      entryDateUTC: this.getDate(),
      bodyweight: this.form.value.bodyweight,
      bodyweightUnit: this.form.value.bodyweightUnit,
      notes: this.form.value.notes
    };
    if (this.workoutId != null) {
      const workoutId = this.workoutId;
      this.workouts.updateWorkout(workoutId, workoutData).subscribe(() => {
        this.toast.setToast({ message: 'Workout updated', domClass: 'bg-success text-light' });
        workoutData.entryDateUTC = localDate;
        this.updateComplete.emit({...workoutData, id: workoutId });
        this.enableSubmit = true;
      })
    } else {
      this.workouts.createWorkout(workoutData).subscribe(workoutId => {
        this.toast.setToast({ message: 'Workout created', domClass: 'bg-success text-light' });
        workoutData.entryDateUTC = localDate;
        this.updateComplete.emit({...workoutData, id: workoutId });
      });
    }
  }
}
