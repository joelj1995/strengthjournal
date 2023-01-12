import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, switchMap, tap, withLatestFrom } from 'rxjs';
import { Country } from '../model/country';
import { ProfileSettings } from '../model/profile-settings';
import { ConfigService } from './config.service';
import { StrengthjournalBaseService } from './strengthjournalbase.service';

@Injectable({
  providedIn: 'root'
})
export class ProfileService extends StrengthjournalBaseService {

  constructor(http: HttpClient, private config: ConfigService) { super(http); }

  getCountries(): Observable<Country[]> {
    return this.http.get<Country[]>(`${this.BASE_URL}/profile/countries`);
  }

  getSettings(): Observable<ProfileSettings> {
    return this.http.get<ProfileSettings>(`${this.BASE_URL}/profile/settings`);
  }

  updateSettings(settings: ProfileSettings): Observable<void> {
    return this.http.post<void>(`${this.BASE_URL}/profile/settings`, settings)
      .pipe(
        tap(() => this.config.refreshConfig())
      );
  }

  updateEmail(newEmail: string): Observable<void> {
    return this.http.put<void>(`${this.BASE_URL}/profile/email`, {newEmail});
  }
  
}
