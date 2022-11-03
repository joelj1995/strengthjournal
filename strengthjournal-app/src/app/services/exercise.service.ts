import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Exercise } from '../model/exercise';
import { StrengthjournalBaseService } from './strengthjournalbase.service';

@Injectable({
  providedIn: 'root'
})
export class ExerciseService extends StrengthjournalBaseService {

  constructor(http: HttpClient) { super(http); }

  getExercises(): Observable<Exercise[]> {
    return this.http.get<Exercise[]>(`${this.BASE_URL}/exercises`);
  }

  createExercise(name: string): Observable<void> {
    return this.http.post<void>(`${this.BASE_URL}/exercises`, {name});
  }

  updateExercise(exerciseId: string, name: string): Observable<void> {
    return this.http.put<void>(`${this.BASE_URL}/exercises/${exerciseId}`, {name});
  }

  deleteExercise(exerciseId: string): Observable<void> {
    return this.http.delete<void>(`${this.BASE_URL}/exercises/${exerciseId}`);
  }

}
