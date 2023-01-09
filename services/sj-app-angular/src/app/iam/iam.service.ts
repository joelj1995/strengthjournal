import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, of } from 'rxjs';
import { Country } from '../model/country';

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

  signup(request: SignupRequest): Observable<SignupResponse> {
    return this.http.post<SignupResponse>(`${this.BASE_URL}/account/create`, request)
      .pipe(
        catchError(err => {
          try {
            return of(err.error as SignupResponse);
          } catch {
            return of({
              result: SignupResponseCode.Unknown
            });
          }
        })
      );
  }

  sendVerification(email: string): Observable<any> {
    return this.http.post(`${this.BASE_URL}/account/send-verification`, { username: email });
  }

  getCountries(): Observable<Country[]> {
    return this.http.get<Country[]>(`${this.BASE_URL}/countries`);
  }

  resetPassword(email: string): Observable<any> {
    return this.http.post(`${this.BASE_URL}/account/reset-password`, { username: email });
  }

}

export interface LoginResponse {
  token: string | null;
  result: LoginStatusCode;
}

export enum LoginStatusCode {
  Success = "Success",
  WrongPassword = "WrongPassword",
  ServiceFailure = "ServiceFailure",
  EmailNotVerified = "EmailNotVerified",
  Unknown = "Unknown"
}

export interface SignupRequest {
  username: string;
  password: string;
  consentCEM: boolean;
  countryCode: string;
}

export interface SignupResponse {
  result: SignupResponseCode
}

export enum SignupResponseCode {
  Success = "Success",
  ValidationError = "ValidationError",
  ServiceFailure = "ServiceFailure",
  Unknown = "Unknown"
}