import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Exercise } from 'src/app/model/exercise';

@Component({
  selector: '[app-set-editor]',
  templateUrl: './set-editor.component.html',
  styleUrls: ['./set-editor.component.css']
})
export class SetEditorComponent implements OnInit {

  @Input()
  exerciseError: boolean = false;

  @Input()
  exerciseList: Exercise[] = [];

  @Input()
  form!: FormGroup;

  constructor() { }

  ngOnInit(): void {
  }

}
