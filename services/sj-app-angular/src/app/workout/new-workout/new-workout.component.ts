import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { WorkoutCreate } from 'src/app/model/workout-create';
import { WorkoutService } from 'src/app/services/workout.service';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { WorkoutCreateUpdateResult } from 'src/app/model/workout-create-update-result';
import { WorkoutUpdate } from 'src/app/model/workout-update';
import { ConfigService } from 'src/app/services/config.service';
import { StrengthJournalContext } from 'src/app/core/strength-journal-context';

@Component({
  selector: 'app-new-workout',
  templateUrl: './new-workout.component.html',
  styleUrls: ['./new-workout.component.css']
})
export class NewWorkoutComponent implements OnInit {
  context: StrengthJournalContext = this.route.snapshot.data['context'];

  workoutData: WorkoutUpdate;
  
  constructor(private workouts: WorkoutService, private router: Router, private route: ActivatedRoute) { 
    this.workoutData = {
      title: '',
      entryDateUTC: new Date(),
      bodyweight: null,
      bodyweightUnit: this.context.config.preferredWeightUnit,
      notes: ''
    }
  }

  ngOnInit(): void { 
    
  }

  updateDone(workoutData: WorkoutCreateUpdateResult) {
    this.router.navigate(['workouts', workoutData.id, 'edit']);
  }

}
