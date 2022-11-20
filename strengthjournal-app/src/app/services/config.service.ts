import { Injectable } from '@angular/core';
import { ProfileSettings } from '../model/profile-settings';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  readonly minVersion: number = 1;

  protected config: any = null;

  constructor() { 
    const configJson = localStorage.getItem('app_config');
    if (configJson) {
      this.config = JSON.parse(configJson);
      if (this.config.Version < this.minVersion)
        throw 'Config version is lower than min supported version'; // TODO: create page to refresh this
    }
    this.config = {
      PreferredWeightUnit: 'kg'
    }
  }

  update(newSettings: ProfileSettings) {
    this.config.PreferredWeightUnit = newSettings.preferredWeightUnit;
  }

  getPreferredWeigthUnit(): string {
    return this.config?.PreferredWeightUnit ?? '';
  }

}
