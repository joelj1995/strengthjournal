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
  systemDefined: boolean = false;

  name: string;

  constructor(private route: ActivatedRoute, private exercise: ExerciseService, private config: ConfigService) {
    const exerciseData = this.route.snapshot.data['exercise'];
    this.name = exerciseData.name;
    this.systemDefined = exerciseData.systemDefined;
    this.id = exerciseData.id;
  }

  displayKg(): boolean {
    return this.config.getPreferredWeigthUnit() == 'kg';
  }

  ngOnInit(): void {

  }

}
