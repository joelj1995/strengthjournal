import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AppConfig } from '../model/app-config';
import { ProfileSettings } from '../model/profile-settings';
import { StrengthjournalBaseService } from './strengthjournalbase.service';

@Injectable({
  providedIn: 'root'
})
export class ConfigService extends StrengthjournalBaseService  {

  configTooOld = false;

  private minVersion: number = 2;
  private config!: AppConfig;
  
  constructor(
    http: HttpClient,
    private router: Router
  ) {
    super(http);
    document.addEventListener('enableFeature', (e: any) => {
      this.config.features.push(e.detail);
    });
  }

  pullUpdate(): Observable<AppConfig> {
    return this.http.get<AppConfig>(`${this.BASE_URL}/profile/config`)
      .pipe(
        tap(config => {
          localStorage.setItem('app_config', JSON.stringify(config));
          this.config = config;
          this.configTooOld = false;
        })
      );
  }

  update(newSettings: ProfileSettings) {
    if (!this.config)
      return;
    this.config.preferredWeightUnit = newSettings.preferredWeightUnit;
  }

  getPreferredWeigthUnit(): string {
    return this.config?.preferredWeightUnit ?? '';
  }

  hasFeature(featureName: string): boolean {
    if (environment.features.includes(featureName))
      return true;
    const remoteFeatures = this.config.features;
    return remoteFeatures.includes(featureName);
  }

  triggerLocalDevError() {
    this.router.navigate(['/error']);
  }

}
