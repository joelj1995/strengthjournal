import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DevLoginComponent } from './pages/dev-login/dev-login.component';
import { TracerComponent } from './tracer/tracer/tracer.component';

const routes: Routes = [
  { path: '', component: TracerComponent },
  { path: 'login', component: DevLoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
