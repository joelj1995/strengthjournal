import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { WorkoutCreateUpdate } from 'src/app/model/workout-create-update';
import { WorkoutService } from 'src/app/services/workout.service';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-new-workout',
  templateUrl: './new-workout.component.html',
  styleUrls: ['./new-workout.component.css']
})
export class NewWorkoutComponent implements OnInit {
  
  constructor(private workouts: WorkoutService, private router: Router) { }

  ngOnInit(): void { }

}
