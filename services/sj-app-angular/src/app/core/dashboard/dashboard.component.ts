import { Component, OnInit } from '@angular/core';
import { WeeklyVolumeReportLine } from 'src/app/model/weekly-volume-report-line';
import { DashboardService } from 'src/app/services/dashboard.service';
import { Chart, ChartConfiguration, ChartItem, registerables } from 'node_modules/chart.js'
import { WeeklyTonnageReportLine } from 'src/app/model/weekly-tonnage-report-line';
import { ConfigService } from 'src/app/services/config.service';
import { AppFeatures } from '../app-features';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  volumeChartLoaded = false;
  tonnageChartLoaded = false;

  constructor(
    private dashboard: DashboardService,
    private config: ConfigService) { }

  ngOnInit(): void {
    this.dashboard.getWeeklyVolumeReport().subscribe(lines => {
      this.createWeeklyVolumeReportChart(lines)
    });
    this.dashboard.getWeeklyTonnageReport().subscribe(lines => {
      this.createWeeklyTonnageReport(lines);
    })
  }

  createWeeklyVolumeReportChart(lines: WeeklyVolumeReportLine[]) {
    Chart.register(...registerables);
    const data = {
      labels: lines.map(l => new Date(l.weekStart)).map(d => `${d.getDate()}/${d.getMonth()+1}`),
      datasets: [{
        label: 'Number of sets',
        backgroundColor: 'rgb(63, 128, 234)',
        borderColor: 'rgb(63, 128, 234)',
        data: lines.map(l => l.numberOfSets),
      }]
    };
    const options = {
      scales: {
        y: {
          beginAtZero: true,
          display: false
        }
      }
    };
    const config: ChartConfiguration = {
      type: 'bar',
      data: data,
      options: options
    }
    const chartItem: ChartItem = document.getElementById('volume-chart') as ChartItem;
    new Chart(chartItem, config);
    this.volumeChartLoaded = true;
  }

  createWeeklyTonnageReport(lines: WeeklyTonnageReportLine[]) {
    Chart.register(...registerables);
    const data = {
      labels: lines.map(l => new Date(l.weekStart)).map(d => `${d.getDate()}/${d.getMonth()+1}`),
      datasets: [{
        label: 'Squat',
        backgroundColor: 'rgb(63, 128, 234)',
        borderColor: 'rgb(63, 128, 234)',
        data: lines.map(l => l.squatTonnage),
      },
      {
        label: 'Bench',
        backgroundColor: 'rgb(217, 83, 79)',
        borderColor: 'rgb(217, 83, 79)',
        data: lines.map(l => l.benchTonnage),
      },
      {
        label: 'Deadlift',
        backgroundColor: 'rgb(233, 179, 103)',
        borderColor: 'rgb(233, 179, 103)',
        data: lines.map(l => l.deadliftTonnage),
      }]
    };
    const options = {
      scales: {
        y: {
          beginAtZero: true,
          display: false
        }
      }
    };
    const config: ChartConfiguration = {
      type: 'bar',
      data: data,
      options: options
    }
    const chartItem: ChartItem = document.getElementById('tonnage-chart') as ChartItem;
    new Chart(chartItem, config)
    this.tonnageChartLoaded = true;
  }

}
