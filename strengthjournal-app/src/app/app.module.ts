import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import {APP_BASE_HREF} from '@angular/common';
import { TracerComponent } from './tracer/tracer/tracer.component';
import { StrengthjournalService } from './services/strengthjournal.service';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    TracerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [{provide: APP_BASE_HREF, useValue: '/app'}, StrengthjournalService],
  bootstrap: [AppComponent]
})
export class AppModule { }
