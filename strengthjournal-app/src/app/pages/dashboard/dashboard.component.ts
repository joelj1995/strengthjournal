import { Component, OnInit } from '@angular/core';
import { WeeklyVolumeReportLine } from 'src/app/model/weekly-volume-report-line';
import { DashboardService } from 'src/app/services/dashboard.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  weeklyVolumeLines: WeeklyVolumeReportLine[] | null = null;

  constructor(private dashboard: DashboardService) { }

  ngOnInit(): void {
    this.dashboard.getWeeklyVolumeReport().subscribe(lines => {
      this.weeklyVolumeLines = lines;
    });
  }

}
