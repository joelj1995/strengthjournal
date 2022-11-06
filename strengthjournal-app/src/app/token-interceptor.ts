import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor() {}
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    var token = localStorage.getItem('app_token');
    
    if (this.isApiUrl(request.url)) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
    }
    
    return next.handle(request);
  }

  isApiUrl(url: string) {
    const origin = window.location.origin;
    const apiUrl = environment.api;
    return url.startsWith(origin) || url.startsWith(apiUrl);
  }
}