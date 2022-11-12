import { Injectable } from '@angular/core';
import { Observable, of, Subscriber } from 'rxjs';
import { ToastMessage } from '../model/toast-message';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  subscribers: Subscriber<ToastMessage>[] = [];

  private toastMessage: Observable<ToastMessage> = new Observable<ToastMessage>(subscriber => {
    this.subscribers.push(subscriber);
  });

  constructor() { }

  setToast(message: ToastMessage) {
    this.subscribers.forEach(sub => {
      sub.next(message);
    });
  }

  getToast(): Observable<ToastMessage> {
    return this.toastMessage;
  }

}
