import { Component } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { environment } from 'src/environments/environment';

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
      if (user == null) {
        if (environment.production) {
          window.location.replace('/login');
        } else {
          this.auth.loginWithRedirect();
        }
        return;
      }
      this.userFullName = user?.name ?? '';
    })
  };
}
