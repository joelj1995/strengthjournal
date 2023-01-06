import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { WorkoutCreate } from 'src/app/model/workout-create';
import { WorkoutService } from 'src/app/services/workout.service';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { WorkoutCreateUpdateResult } from 'src/app/model/workout-create-update-result';
import { WorkoutUpdate } from 'src/app/model/workout-update';
import { ConfigService } from 'src/app/services/config.service';

@Component({
  selector: 'app-new-workout',
  templateUrl: './new-workout.component.html',
  styleUrls: ['./new-workout.component.css']
})
export class NewWorkoutComponent implements OnInit {

  workoutData: WorkoutUpdate;
  
  constructor(private workouts: WorkoutService, private router: Router, private config: ConfigService) { 
    this.workoutData = {
      title: '',
      entryDateUTC: new Date(),
      bodyweight: null,
      bodyweightUnit: this.config.getPreferredWeigthUnit(),
      notes: ''
    }
  }

  ngOnInit(): void { 
    
  }

  updateDone(workoutData: WorkoutCreateUpdateResult) {
    this.router.navigate(['workouts', workoutData.id, 'edit']);
  }

}
