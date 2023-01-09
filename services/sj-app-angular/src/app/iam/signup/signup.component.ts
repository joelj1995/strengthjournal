import { Component, OnInit } from '@angular/core';
import { Country } from 'src/app/model/country';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  showPasswordRules: boolean = false;

  countryList: Country[] = [
    { name: 'Canada', code: 'CA' },
  ]

  form = {
    email: '',
    password: '',
    password2: '',
    consentCEM: true,
    countryCode: ''
  }

  constructor() { }

  ngOnInit(): void {
  }

  onSubmit() {
    console.log(this.form);
  }

}
