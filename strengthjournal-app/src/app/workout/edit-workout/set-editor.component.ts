import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Exercise } from 'src/app/model/exercise';

@Component({
  selector: '[app-set-editor]',
  templateUrl: './set-editor.component.html',
  styleUrls: ['./set-editor.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SetEditorComponent implements OnInit {

  @Input()
  exerciseError: boolean = false;

  @Input()
  exerciseList: Exercise[] = [];

  @Input()
  form!: FormGroup;

  @Output()
  newExerciseSelected = new EventEmitter;

  constructor() { }

  ngOnInit(): void {
  }

  onExerciseSelectionChange(e: any) {
    this.newExerciseSelected.emit();
  }

}
