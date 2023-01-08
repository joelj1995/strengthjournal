import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of, ReplaySubject, tap } from 'rxjs';
import { AppConfig } from '../model/app-config';
import { ProfileSettings } from '../model/profile-settings';
import { StrengthjournalBaseService } from './strengthjournalbase.service';

@Injectable({
  providedIn: 'root'
})
export class ConfigService extends StrengthjournalBaseService  {

  configTooOld = false;
  configLoaded = false;

  private minVersion: number = 2;
  private configSubject$ = new ReplaySubject<AppConfig>(1);

  private featuresSubject$ = new ReplaySubject<string[]>(1);
  features$ = this.featuresSubject$.asObservable();
  
  constructor(
    http: HttpClient,
    private router: Router
  ) {
    super(http);

    document.addEventListener('enableFeature', (e: any) => { });
    
    this.featuresSubject$.next([]);
  }

  public get config$() {
    if (!this.configLoaded) {
      this.refreshConfig();
      this.configLoaded = true;
    }
    return this.configSubject$.asObservable();
  }

  triggerLocalDevError() {
    this.router.navigate(['/error']);
  }

  refreshConfig() {
    this.http.get<AppConfig>(`${this.BASE_URL}/profile/config`)
      .pipe(
        tap(config => console.log('new config pulled : ' + JSON.stringify(config)))
      )
      .subscribe(
        config => this.configSubject$.next(config)
      );
  }

}
