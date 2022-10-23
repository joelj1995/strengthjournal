import { Component } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'strengthjournal-app';
  userFullName = '';

  constructor(public auth: AuthService) {
    auth.user$.subscribe(user => {
      this.userFullName = user?.name ?? '';
    })
  };
}
