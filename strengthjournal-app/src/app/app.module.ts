import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import {APP_BASE_HREF} from '@angular/common';
import { TracerComponent } from './tracer/tracer/tracer.component';
import { StrengthjournalService } from './services/strengthjournal.service';
import { HttpClientModule } from '@angular/common/http';
import { DevLoginComponent } from './pages/dev-login/dev-login.component';
import { AuthModule } from '@auth0/auth0-angular';

@NgModule({
  declarations: [
    AppComponent,
    TracerComponent,
    DevLoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AuthModule.forRoot({
      domain: 'dev-bs65rtlog25jigd0.us.auth0.com',
      clientId: 'LdMw0S4EL13LvL4SZJOPRCSZo5cZJ3zD'
    }),
  ],
  providers: [{provide: APP_BASE_HREF, useValue: '/app'}, StrengthjournalService],
  bootstrap: [AppComponent]
})
export class AppModule { }
