import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StrengthjournalBaseService } from './strengthjournalbase.service';

@Injectable({
  providedIn: 'root'
})
export class WorkoutService extends StrengthjournalBaseService {

  constructor(http: HttpClient) { super(http); }

  createWorkoutNow(): Observable<string> {
    return this.http.post<string>(`${this.BASE_URL}/workouts`, { startDate: new Date().toISOString() });
  }

}
