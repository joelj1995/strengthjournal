import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
  HttpStatusCode
} from '@angular/common/http';
import { catchError, map, Observable, tap, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';

export class HandledHttpError implements Error {
  readonly name = "HandledHttpError";
  readonly message: string;
  constructor(messsage: string) {
    this.message = messsage;
  }
}

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor() { }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    var token = localStorage.getItem('app_token');

    if (this.isApiUrl(request.url)) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
    }

    return next.handle(request).pipe(
      map((event: HttpEvent<any>) => {
        return event;
      }),
      catchError(
        (
          httpErrorResponse: HttpErrorResponse,
          _: Observable<HttpEvent<any>>
        ) => {
          if (httpErrorResponse.status === HttpStatusCode.Unauthorized && this.isApiUrl(httpErrorResponse.url ?? '')) {
            window.location.replace('/login');
            return throwError(() => new HandledHttpError(''));
          }
          return throwError(() => httpErrorResponse);
        }
      )
    );
  }

  isApiUrl(url: string) {
    const origin = window.location.origin;
    const apiUrl = environment.api;
    return url.startsWith(origin) || url.startsWith(apiUrl);
  }
}