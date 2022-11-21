import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { AppConfig } from '../model/app-config';
import { ProfileSettings } from '../model/profile-settings';
import { StrengthjournalBaseService } from './strengthjournalbase.service';

@Injectable({
  providedIn: 'root'
})
export class ConfigService extends StrengthjournalBaseService  {

  readonly minVersion: number = 1;

  protected config: AppConfig | null = null;

  public configTooOld = false;

  constructor(http: HttpClient) {
    super(http);
    const configJson = localStorage.getItem('app_config');
    if (configJson) {
      const config = JSON.parse(configJson);
      if (config.version < this.minVersion)
        this.configTooOld = true
      else
        this.config = config;
    }
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

}
