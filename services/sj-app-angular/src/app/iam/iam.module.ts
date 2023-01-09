import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { SignupComponent } from './signup/signup.component';
import { SignupSuccessComponent } from './signup-success/signup-success.component';
import { CountriesResolver } from './countries.resolver';

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
  }
]

@NgModule({
  declarations: [
    LoginComponent,
    SignupComponent,
    SignupSuccessComponent
  ],
  imports: [
    FormsModule,
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class IamModule { }
