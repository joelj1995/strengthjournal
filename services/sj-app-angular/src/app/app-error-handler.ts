import { ErrorHandler, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { v4 as uuidv4 } from 'uuid';
import { ConfigService } from './services/config.service';

@Injectable()
export class AppErrorHandler implements ErrorHandler {

  constructor(private router: Router, private config: ConfigService) { }

  handleError(error: any) {
    if (error.name == 'HandledHttpError') {
      console.error(error);
      console.log('Handled 401 error. Skipping handler');
      return;
    }
    const errorId = uuidv4();
    try {
      console.error(error);
      const errorData = {
        id: errorId,
        ngData: error.stack || error.message,
        ngVersion: environment.version
      };
      localStorage.setItem('app_error', JSON.stringify(errorData));
      console.debug(errorData);
    } catch {
      console.error('Failed to set message data.')
    }
    if (environment.production) {
      window.location.replace(`/app-exception?errorId=${errorId}`);
    } else {
      this.config.triggerLocalDevError();
    }
  }
}