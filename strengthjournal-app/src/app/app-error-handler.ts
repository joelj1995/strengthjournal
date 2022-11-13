import { ErrorHandler, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { v4 as uuidv4 } from 'uuid';

@Injectable()
export class AppErrorHandler implements ErrorHandler {
  handleError(error: any) {
    console.error(error);
    const errorId = uuidv4();
    const errorData = {
      id: errorId,
      ngData: error.stack,
      ngVersion: environment.version
    };
    localStorage.setItem('app_error', JSON.stringify(errorData));
    if (environment.production) {
      if (error.name != 'HandledHttpError') window.location.replace(`/app-exception?errorId=${errorId}`);
    } else {
      alert('Exception hit. See error logs for details.')
    }
  }
}