import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SpinnerService {

  enableSpinner$ = new BehaviorSubject(false);

  constructor() { }

  getSpinnerEnabled(): Observable<boolean> {
    return this.enableSpinner$;
  }

  setSpinnerEnabled(enabled: boolean) {
    return this.enableSpinner$.next(enabled);
  }

}
