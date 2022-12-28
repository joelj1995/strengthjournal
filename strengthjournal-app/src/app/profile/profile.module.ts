import { NgModule } from '@angular/core';
import { ProfileComponent } from './profile/profile.component';
import { SharedModule } from '../shared/shared.module';
import { RouterModule, Routes } from '@angular/router';
import { ProfileResolver } from './profile.resolver';

const routes: Routes = [
  { 
    path: '', 
    component: ProfileComponent ,
    resolve: { profile: ProfileResolver }
  },
]

@NgModule({
  declarations: [
    ProfileComponent
  ],
  imports: [
    SharedModule,
    RouterModule.forChild(routes)
  ],
  exports: [
  ]
})
export class ProfileModule { }
