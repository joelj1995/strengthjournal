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
}
