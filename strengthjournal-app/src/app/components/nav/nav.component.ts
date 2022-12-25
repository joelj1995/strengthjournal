import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(public auth: AuthService) { }

  ngOnInit(): void {
    (window as any).feather.replace();
  }

  logout() {
    if (environment.useResourceOwnerFlow) {
      localStorage.clear();
      window.location.replace('/');
    }
    else {
      this.auth.logout({ returnTo: document.location.origin });
    }
  }

}
