import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Country } from 'src/app/model/country';
import { IamService, SignupResponseCode } from '../iam.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  processing: boolean = false;
  showPasswordRules: boolean = false;
  submitError: string | undefined;

  countryList: Country[] = [
    { name: 'Canada', code: 'CA' },
  ]

  form = {
    email: '',
    password: '',
    password2: '',
    consentCEM: true,
    countryCode: 'CA'
  }

  constructor(
    private route: ActivatedRoute,
    private iam: IamService,
    private router: Router
  ) { 
    this.countryList = this.route.snapshot.data['countryList'];
  }

  ngOnInit(): void {
  }

  onSubmit() {
    this.iam.signup({ 
      username: this.form.email, 
      password: this.form.password, 
      consentCEM: this.form.consentCEM, 
      countryCode: this.form.countryCode }
    ).subscribe(response => {
      this.processing = false;
      switch (response.result) {
        case SignupResponseCode.Success: {
          this.router.navigate(['/iam/signup-success']);
          break;
        }
        case SignupResponseCode.ValidationError: {
          this.showPasswordRules = true;
          break;
        }
        default: {
          this.submitError = "There was an error processing your account creation. Please try again in a few minutes."
        }
      }
    });
    console.log(this.form);
  }

  formError(): string | null {
    if (this.form.email == '') {
      return 'Email address is required';
    }
    if (this.form.password == '') {
      return 'Password is required'
    }
    if (this.form.password != this.form.password2) {
      return 'Passwords do not match';
    }
    return null;
  }

}
