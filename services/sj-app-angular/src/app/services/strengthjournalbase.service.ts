import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Exercise } from '../model/exercise';

@Injectable({
  providedIn: 'root'
})
export class StrengthjournalBaseService {

  protected BASE_URL: string = environment.api;

  constructor(
    protected http: HttpClient
  ) { }
  
}
