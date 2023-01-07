import { Component, HostListener, OnInit } from '@angular/core';
import { Event, NavigationStart, NavigationEnd, NavigationCancel, Router, RouterEvent } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';
import { filter } from 'rxjs';
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
  localDevError = false;

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
    this.userFullName = 'FIX THIS'; // TODO: get this from the config
  }

  constructor(
    public auth: AuthService, 
    private router: Router, 
    private config: ConfigService,
    private spinner: SpinnerService
  ) {
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
    this.router.events
      .pipe(
        filter((e: Event): e is RouterEvent => e instanceof RouterEvent)
      )
      .subscribe((routerEvent: RouterEvent) => {
        this.checkRouterEvent(routerEvent);
      });
    this.config.localDevError$.subscribe(isError => {
      this.localDevError = isError;
    });
    this.configUpdating = true;
    this.spinner.setSpinnerEnabled(true);
    this.config.pullUpdate().subscribe(() => {
      this.configUpdating = false;
      this.spinner.setSpinnerEnabled(false);
    });
  }

  checkRouterEvent(routerEvent: RouterEvent) {
    if (routerEvent instanceof NavigationStart)
      this.spinner.setSpinnerEnabled(true);
    
    if (routerEvent instanceof NavigationEnd || routerEvent instanceof NavigationCancel)
      this.spinner.setSpinnerEnabled(false);
  }

  toggleNav() {
    this.navCollapsed = !this.navCollapsed;
  }
}
