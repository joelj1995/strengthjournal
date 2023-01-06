import { Injectable } from '@angular/core';
import { delay, mergeWith, Observable, scan, Subject } from 'rxjs';
import { ToastMessage } from '../model/toast-message';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  private newToast = new Subject<ToastMessage>();
  private popToast = this.newToast.pipe(
    delay(3000)
  );

  setToast(message: ToastMessage) {
    this.newToast.next(message);
  }

  getToast(): Observable<ToastMessage[]> {
    return this.newToast.pipe(
      mergeWith(this.popToast),
      scan((acc, next) => acc.length > 0 && acc[0] == next ? acc.slice(1) : [...acc, next], [] as ToastMessage[])
    );
  }

}
