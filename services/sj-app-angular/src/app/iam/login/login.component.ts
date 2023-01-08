import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { catchError } from 'rxjs';
import { ToastService } from 'src/app/services/toast.service';
import { IamService, LoginResponse, LoginStatusCode } from '../iam.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  readonly loginErrorMessages = {
    'WrongPassword': 'The username and password combination you used is not correct.',
    'ServiceFailure': 'There was an error processing your login. Please try again in a few minutes.',
    'Unknown': 'There was an error processing your loging. Please try again in a few minutes.'
  }

  formEmail: string = '';
  formPassword: string = '';

  unverifiedEmail: string | null = null;

  processing: boolean = false;

  constructor(
    private iam: IamService,
    private toast: ToastService,
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.unverifiedEmail = null;
    this.processing = true;
    this.iam.login(this.formEmail, this.formPassword)
      .subscribe(data => this.processLoginResponse(data));
  }

  processLoginResponse(data: LoginResponse) {
    this.processing = false;
    switch (data.result) {
      case LoginStatusCode.Success: {
        if (!data.token) throw 'Got a null token for a successful login result';
        const token = data.token;
        localStorage.setItem('app_token', token);
        this.processing = false;
        this.toast.setToast({ message: 'Login succeeded', domClass: 'bg-success text-light' });
        this.router.navigate(['/']);
        break;
      }
      case LoginStatusCode.WrongPassword:
      case LoginStatusCode.ServiceFailure:
      case LoginStatusCode.Unknown: {
        this.toast.setToast({ message: this.loginErrorMessages[data.result], domClass: 'bg-danger text-light' });
        break;
      }
      case LoginStatusCode.EmailNotVerified: {
        this.unverifiedEmail = this.formEmail;
      }
    }
  }

  resendVerification() {
    if (!this.unverifiedEmail) return;
    this.toast.setToast({ message: 'Sending verification email...', domClass: 'bg-info text-light' })
    this.iam.sendVerification(this.unverifiedEmail)
      .subscribe({
        next: () => this.toast.setToast({ message: 'Verification sent!', domClass: 'bg-success text-light' }),
        error: () => this.toast.setToast({ message: 'An error occured re-sending the verification email. Please try again later.', domClass: 'bg-danger text-light' })
      });
  }

}
