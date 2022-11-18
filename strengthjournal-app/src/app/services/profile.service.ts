import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ProfileSettings } from '../model/profile-settings';
import { StrengthjournalBaseService } from './strengthjournalbase.service';

@Injectable({
  providedIn: 'root'
})
export class ProfileService extends StrengthjournalBaseService {

  constructor(http: HttpClient) { super(http); }

  getSettings(): Observable<ProfileSettings> {
    return this.http.get<ProfileSettings>(`${this.BASE_URL}/profile/settings`);
  }

  updateSettings(settings: ProfileSettings): Observable<void> {
    return this.http.post<void>(`${this.BASE_URL}/profile/settings`, settings);
  }

  resetPasword(): Observable<void> {
    return this.http.put<void>(`${this.BASE_URL}/profile/password-reset`, null);
  }
  
}