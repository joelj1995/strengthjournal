import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Settings } from 'http2';
import { forkJoin, map, Observable, of } from 'rxjs';
import { Country } from '../model/country';
import { ResolvedProfile } from '../model/vm/resolved-profile';
import { ProfileService } from '../services/profile.service';

@Injectable({
  providedIn: 'root'
})
export class ProfileResolver implements Resolve<ResolvedProfile> {

  constructor(private profile: ProfileService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ResolvedProfile> {
    const settings = this.profile.getSettings();
    const countries = this.profile.getCountries();
    return forkJoin([settings, countries]).pipe(
      map(results => {
        return {
          settings: results[0] as Settings,
          countryList: results[1] as Country[]
        } as ResolvedProfile
      })
    );
  }
}
