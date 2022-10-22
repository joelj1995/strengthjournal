import { Component, OnInit } from '@angular/core';
import { Exercise } from 'src/app/model/exercise';
import { StrengthjournalService } from 'src/app/services/strengthjournal.service';

@Component({
  selector: 'app-tracer',
  templateUrl: './tracer.component.html',
  styleUrls: ['./tracer.component.css']
})
export class TracerComponent implements OnInit {

  constructor(private strengthjournal: StrengthjournalService) { }

  exercises: Exercise[] = [];

  ngOnInit(): void {
    this.strengthjournal.getExercises().subscribe(exercises => {
      this.exercises = exercises;
    })
  }

}
