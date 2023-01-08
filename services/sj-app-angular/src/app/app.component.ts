import { Component, HostListener, OnInit } from '@angular/core';
import { Event, NavigationStart, NavigationEnd, NavigationCancel, Router, RouterEvent, ActivatedRoute } from '@angular/router';
import { filter, of, switchMap } from 'rxjs';
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
  noNav: boolean = true;

  navCollapsed: boolean = false;
  configUpdating: boolean = false;
  showSpinner: boolean = false;

  title = 'strengthjournal-app';
  userFullName = '';

  @HostListener('window:resize', ['$event'])
  onResize() {
   this.screenWidth = window.innerWidth;
  }

  constructor(
    private router: Router, 
    private spinner: SpinnerService,
    private activatedRoute: ActivatedRoute
  ) {
    this.router.events.subscribe(ev => {
      if (this.screenWidth < this.lgBreakpoint)
        this.navCollapsed = false;
    })
    this.onResize();
    this.router.events
      .pipe(
        filter(ev => ev instanceof NavigationEnd),
        switchMap(() => this.activatedRoute.firstChild?.data ?? of(null))
      )
      .subscribe(routeData => {
        if (routeData) {
          this.noNav = routeData['noNav'] ?? false;
        }
      });
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
