import { Component, HostListener, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterEvent } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';
import { environment } from 'src/environments/environment';
import { ConfigService } from './services/config.service';
import { SpinnerService } from './services/spinner.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  readonly lgBreakpoint: number = 992; // defined as map-get($grid-breakpoints, 'lg') by appstack
  screenWidth: number = 0;

  navCollapsed: boolean = false;
  configUpdating: boolean = false;
  showSpinner: boolean = false;

  title = 'strengthjournal-app';
  userFullName = '';

  @HostListener('window:resize', ['$event'])
  onResize() {
   this.screenWidth = window.innerWidth;
  }

  redirectLoginFlow() {
    this.auth.user$.subscribe(user => {
      if (user == null) {
          this.auth.loginWithRedirect();
        return;
      }
      this.userFullName = user?.name ?? '';
      localStorage.setItem('app_username', this.userFullName);
    });
  }

  resourceOwnerLoginFlow() {
    this.userFullName = localStorage.getItem('app_username') ?? '';
  }

  constructor(
    public auth: AuthService, 
    private router: Router, 
    private config: ConfigService,
    private spinner: SpinnerService) {
    if (environment.useResourceOwnerFlow) {
      this.resourceOwnerLoginFlow();
    } else {
      this.redirectLoginFlow();
    }
    this.router.events.subscribe(ev => {
      if (this.screenWidth < this.lgBreakpoint)
        this.navCollapsed = false;
    })
    this.onResize();
  }
  
  ngOnInit(): void {
    this.spinner.getSpinnerEnabled().subscribe(spinnerEnabled => {
      this.showSpinner = spinnerEnabled;
    });
    if (this.config.configTooOld) {
      this.configUpdating = true;
      this.spinner.setSpinnerEnabled(true);
      this.config.pullUpdate().subscribe(() => {
        this.configUpdating = false;
        this.spinner.setSpinnerEnabled(false);
      });
    }
  }

  toggleNav() {
    this.navCollapsed = !this.navCollapsed;
  }
}
