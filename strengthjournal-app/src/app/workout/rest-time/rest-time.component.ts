import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-rest-time',
  templateUrl: './rest-time.component.html',
  styleUrls: ['./rest-time.component.css']
})
export class RestTimeComponent implements OnInit {

  @Input()
  lastSetLogged!: Date;

  seconds: string = '';
  minutes: string = '';

  constructor() { 
    this.renderTimerOnLoop();
  }

  renderTimerOnLoop() {
    const timer = setInterval(() => {
      const currentTime = new Date(Date());
      const interval = currentTime.getTime() - this.lastSetLogged.getTime();
      this.minutes = this.padTimeTwoDecimals(Math.floor(interval / (1000 * 60)));
      this.seconds = this.padTimeTwoDecimals(Math.floor((interval % (1000 * 60)) / 1000));
    }, 1000);
  }

  padTimeTwoDecimals(timeComponent: number): string {
    if (timeComponent < 10) {
      return '0' + timeComponent;
    }
    return timeComponent.toString();
  }

  ngOnInit(): void {
  }

}
