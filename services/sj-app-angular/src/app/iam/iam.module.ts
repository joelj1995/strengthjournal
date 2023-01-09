import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { SignupComponent } from './signup/signup.component';
import { SignupSuccessComponent } from './signup-success/signup-success.component';
import { CountriesResolver } from './countries.resolver';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ForgotPasswordSuccessComponent } from './forgot-password-success/forgot-password-success.component';

const routes: Routes = [
  { 
    path: 'login', 
    component: LoginComponent 
  },
  {
    path: 'signup',
    component: SignupComponent,
    resolve: { countryList: CountriesResolver }
  },
  {
    path: 'signup-success',
    component: SignupSuccessComponent
  },
  {
    path: 'forgot-password',
    component: ForgotPasswordComponent
  },
  {
    path: 'forgot-password-success',
    component: ForgotPasswordSuccessComponent
  }
]

@NgModule({
  declarations: [
    LoginComponent,
    SignupComponent,
    SignupSuccessComponent,
    ForgotPasswordComponent,
    ForgotPasswordSuccessComponent
  ],
  imports: [
    FormsModule,
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class IamModule { }
