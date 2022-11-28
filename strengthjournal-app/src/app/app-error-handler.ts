import { ErrorHandler, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { v4 as uuidv4 } from 'uuid';

@Injectable()
export class AppErrorHandler implements ErrorHandler {
  handleError(error: any) {
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
      if (error.name != 'HandledHttpError') window.location.replace(`/app-exception?errorId=${errorId}`);
    } else {
      alert('Exception hit. See error logs for details.')
    }
  }
}