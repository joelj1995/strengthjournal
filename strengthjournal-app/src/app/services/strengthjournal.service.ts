import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Exercise } from '../model/exercise';

@Injectable({
  providedIn: 'root'
})
export class StrengthjournalService {

  private BASE_URL: string = environment.production ? '/api' : 'https://localhost:7080/api';

  constructor(
    private http: HttpClient
  ) { }

  getExercises(): Observable<Exercise[]> {
    return this.http.get<Exercise[]>(`${this.BASE_URL}/exercises`);
  }
}
