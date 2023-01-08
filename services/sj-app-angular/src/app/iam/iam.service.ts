import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IamService {

  private readonly BASE_URL = '/services/iam'

  constructor(private http: HttpClient) { }

  login(username: string, password: string): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.BASE_URL}/account/login`, { username, password })
      .pipe(
        catchError(err => {
          try {
            return of(err.error as LoginResponse);
          } catch {
            return of({
              token: null,
              result: LoginStatusCode.Unknown
            });
          }
        })
      );
  }

  sendVerification(email: string): Observable<any> {
    return this.http.post(`${this.BASE_URL}/account/send-verification`, { username: email });
  }

}

export interface LoginResponse {
  token: string | null;
  result: LoginStatusCode
}

export enum LoginStatusCode {
  Success = "Success",
  WrongPassword = "WrongPassword",
  ServiceFailure = "ServiceFailure",
  EmailNotVerified = "EmailNotVerified",
  Unknown = "Unknown"
}