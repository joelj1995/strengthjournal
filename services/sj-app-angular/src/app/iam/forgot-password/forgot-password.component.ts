import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastService } from 'src/app/services/toast.service';
import { IamService } from '../iam.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  formEmail: string = '';
  processing: boolean = false;

  constructor(
    private iam: IamService,
    private router: Router,
    private toast: ToastService
  ) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.iam.resetPassword(this.formEmail)
      .subscribe({
        next: () => {
          this.router.navigate(['/iam/forgot-password-success']);
        },
        error: () => {
          this.toast.setToast({ message: 'There was an error initiating the password reset', domClass: 'bg-danger text-light' });
          this.processing = false;
        }
      });
  }

}
