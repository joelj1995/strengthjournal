import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ConfigService } from 'src/app/services/config.service';
import { ExerciseService } from 'src/app/services/exercise.service';

@Component({
  selector: 'app-view-exercise',
  templateUrl: './view-exercise.component.html',
  styleUrls: ['./view-exercise.component.css']
})
export class ViewExerciseComponent implements OnInit {

  id: string | null = null;

  name: string = '';

  constructor(private route: ActivatedRoute, private exercise: ExerciseService, private config: ConfigService) { }

  displayKg(): boolean {
    return this.config.getPreferredWeigthUnit() == 'kg';
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.exercise.getExercise(params['id']).subscribe(e => {
        this.name = e.name;
      });
    });
  }

}
