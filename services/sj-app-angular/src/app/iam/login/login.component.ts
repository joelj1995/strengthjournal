import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { catchError } from 'rxjs';
import { ToastService } from 'src/app/services/toast.service';
import { IamService } from '../iam.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  formEmail: string = '';
  formPassword: string = '';

  processing: boolean = false;

  constructor(
    private iam: IamService,
    private toast: ToastService,
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.processing = true;
    this.iam.login(this.formEmail, this.formPassword)
      .subscribe({
        next: (data) => {
          const token = data.token;
          localStorage.setItem('app_token', token);
          this.processing = false;
          this.toast.setToast({ message: 'Login succeeded', domClass: 'bg-success text-light' });
          this.router.navigate(['/']);
        },
        error: () => {
          this.processing = false;
          this.toast.setToast({ message: 'Login failed. Please confirm your username and password.', domClass: 'bg-warning text-light' });
        } 
      })
  }

}
