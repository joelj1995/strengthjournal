import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { WorkoutSet } from 'src/app/model/workout-set';
import { WorkoutService } from 'src/app/services/workout.service';

@Component({
  selector: 'app-edit-workout',
  templateUrl: './edit-workout.component.html',
  styleUrls: ['./edit-workout.component.css']
})
export class EditWorkoutComponent implements OnInit {

  id: string = '';
  setList: WorkoutSet[] = [];
  constructor(private route: ActivatedRoute, private workouts: WorkoutService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.setList = [];
      this.workouts.getWorkoutSets(this.id).subscribe(sets => {
        this.setList = sets;
      })
    });
  }

}
