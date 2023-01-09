import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { Country } from '../model/country';
import { IamService } from './iam.service';

@Injectable({
  providedIn: 'root'
})
export class CountriesResolver implements Resolve<Country[]> {

  constructor(private iam: IamService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Country[]> {
    return this.iam.getCountries();
  }
}
