import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DataPage } from '../model/data-page';
import { Exercise } from '../model/exercise';
import { ExerciseHistory } from '../model/exercise-history';
import { StrengthjournalBaseService } from './strengthjournalbase.service';

@Injectable({
  providedIn: 'root'
})
export class ExerciseService extends StrengthjournalBaseService {

  constructor(http: HttpClient) { super(http); }

  getExercises(pageNumber: number, perPage: number): Observable<DataPage<Exercise>> {
    return this.http.get<DataPage<Exercise>>(`${this.BASE_URL}/exercises?pageNumber=${pageNumber}&perPage=${perPage}`);
  }

  getAllExercises(): Observable<DataPage<Exercise>> {
    return this.http.get<DataPage<Exercise>>(`${this.BASE_URL}/exercises?allRecords=true`);
  }

  getExerciseHistory(exerciseId: string): Observable<ExerciseHistory[]> {
    return this.http.get<ExerciseHistory[]>(`${this.BASE_URL}/exercises/${exerciseId}/history`);
  }

  createExercise(name: string): Observable<void> {
    return this.http.post<void>(`${this.BASE_URL}/exercises`, { name, parentExerciseId: null });
  }

  updateExercise(exerciseId: string, name: string): Observable<void> {
    return this.http.put<void>(`${this.BASE_URL}/exercises/${exerciseId}`, { name, parentExerciseId: null });
  }

  deleteExercise(exerciseId: string): Observable<void> {
    return this.http.delete<void>(`${this.BASE_URL}/exercises/${exerciseId}`);
  }

}
