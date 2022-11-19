import { Component, OnInit } from '@angular/core';
import { WeeklyVolumeReportLine } from 'src/app/model/weekly-volume-report-line';
import { DashboardService } from 'src/app/services/dashboard.service';
import { Chart, ChartConfiguration, ChartItem, registerables } from 'node_modules/chart.js'

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
      this.createWeeklyVolumeReportChart(lines)
    });
  }

  createWeeklyVolumeReportChart(lines: WeeklyVolumeReportLine[]) {
    Chart.register(...registerables);
    const data = {
      labels: lines.map(l => new Date(l.weekStart).toLocaleDateString("en-US")),
      datasets: [{
        label: 'Nubmer of sets',
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
    const chartItem: ChartItem = document.getElementById('my-chart') as ChartItem;
    new Chart(chartItem, config)
  }

}
