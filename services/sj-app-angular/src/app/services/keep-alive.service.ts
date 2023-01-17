import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, forkJoin, interval, mergeMap, Observable, of, startWith } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class KeepAliveService {

  public keepAlivePoll$ = interval(1000 * 60 * 1)
    .pipe(
      startWith(-1),
      mergeMap(() => forkJoin([this.pingIam(), this.pingJournal()])),
      catchError(e => {
        console.error(`keep alive failed`);
        console.error(e);
        return of(['', '']);
      })
    );

  constructor(private http: HttpClient) {  }

  private pingIam(): Observable<string> {
    return this.http.get('/services/iam/health/ping', { responseType: 'text' });
  }

  private pingJournal(): Observable<string> {
    return this.http.get('/api/journal/health/ping', { responseType: 'text' });
  }

}
