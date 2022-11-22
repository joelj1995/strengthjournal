import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of, Subscriber } from 'rxjs';
import { ToastMessage } from '../model/toast-message';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  private toastMessages: ToastMessage[] = [];
  private toastMessagesSubject = new BehaviorSubject<ToastMessage[]>([]);

  setToast(message: ToastMessage) {
    this.toastMessages = [message, ...this.toastMessages];
    this.toastMessagesSubject.next(this.toastMessages);
    setTimeout(() => {
      this.toastMessages.pop();
      this.toastMessagesSubject.next(this.toastMessages);
    }, 3000);
  }

  getToast(): Observable<ToastMessage[]> {
    return this.toastMessagesSubject;
  }

}
