import { Component, EventEmitter, Inject, Input, OnInit, Output } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { DOCUMENT } from '@angular/common';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  @Output() navToggleClicked: EventEmitter<any> = new EventEmitter();

  @Input() userFullName: string = '';

  constructor(@Inject(DOCUMENT) public document: Document) { }

  ngOnInit(): void {
  }

}
