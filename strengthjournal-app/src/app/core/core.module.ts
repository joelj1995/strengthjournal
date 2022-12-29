import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavComponent } from './nav/nav.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { ToastComponent } from './toast/toast.component';
import { NotFoundComponent } from './special/not-found.component';
import { BlockingSpinnerComponent } from './blocking-spinner/blocking-spinner.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


@NgModule({
  declarations: [
    NavComponent,
    DashboardComponent,
    HeaderComponent,
    FooterComponent,
    ToastComponent,
    NotFoundComponent,
    BlockingSpinnerComponent
  ],
  imports: [
    RouterModule,
    CommonModule,
    SharedModule,
    BrowserAnimationsModule
  ],
  exports: [
    NavComponent,
    DashboardComponent,
    HeaderComponent,
    FooterComponent,
    ToastComponent,
    NotFoundComponent,
    BlockingSpinnerComponent,
    BrowserAnimationsModule
  ]
})
export class CoreModule { }
