import { Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { ConfigService } from 'src/app/services/config.service';
import { catchError, map, Observable } from 'rxjs';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  @Output() navToggleClicked: EventEmitter<any> = new EventEmitter();

  get tryGetUsername$(): Observable<string> {
    return this.config.config$
      .pipe(
        map(c => c.userName),
        catchError(() => '')
      );
  } 

  constructor(
    @Inject(DOCUMENT) public document: Document,
    private config: ConfigService) { }

  ngOnInit(): void {
  }

}
