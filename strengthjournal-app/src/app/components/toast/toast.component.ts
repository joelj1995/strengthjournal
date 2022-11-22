import { Component, OnInit } from '@angular/core';
import { debounceTime, tap } from 'rxjs';
import { ToastMessage } from 'src/app/model/toast-message';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'app-toast',
  templateUrl: './toast.component.html',
  styleUrls: ['./toast.component.css']
})
export class ToastComponent implements OnInit {

  toastMessages: ToastMessage[] = [];

  constructor(private toast: ToastService) { }

  ngOnInit(): void {
    this.toast.getToast()
    .subscribe(messages => {
      this.toastMessages = messages;
    });
  }

}
