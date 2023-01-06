import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { WeeklyTonnageReportLine } from '../model/weekly-tonnage-report-line';
import { WeeklyVolumeReportLine } from '../model/weekly-volume-report-line';
import { StrengthjournalBaseService } from './strengthjournalbase.service';

@Injectable({
  providedIn: 'root'
})
export class DashboardService extends StrengthjournalBaseService {

  constructor(http: HttpClient) { super(http); }

  getWeeklyVolumeReport(): Observable<WeeklyVolumeReportLine[]> {
    return this.http.get<WeeklyVolumeReportLine[]>(`${this.BASE_URL}/dashboard/weekly-volume-report`);
  }

  getWeeklyTonnageReport(): Observable<WeeklyTonnageReportLine[]> {
    return this.http.get<WeeklyTonnageReportLine[]>(`${this.BASE_URL}/dashboard/weekly-tonnage-report`);
  }

}
