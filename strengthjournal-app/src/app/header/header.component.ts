import { Component, Inject, Input, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { DOCUMENT } from '@angular/common';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  @Input() userFullName: string = '';

  logout() {
    if (environment.useResourceOwnerFlow) {
      localStorage.clear();
      window.location.replace('/');
    }
    else {
      this.auth.logout({ returnTo: document.location.origin });
    }
  }

  constructor(@Inject(DOCUMENT) public document: Document, public auth: AuthService) { }

  ngOnInit(): void {
  }

}
