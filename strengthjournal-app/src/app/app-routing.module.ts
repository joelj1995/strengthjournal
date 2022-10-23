import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DevLoginComponent } from './pages/dev-login/dev-login.component';

const routes: Routes = [
  { path: 'login', component: DevLoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
