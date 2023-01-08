import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { map, Observable, of, take, withLatestFrom } from 'rxjs';
import { ConfigService } from '../services/config.service';
import { StrengthJournalContext } from './strength-journal-context';

@Injectable({
  providedIn: 'root'
})
export class ContextResolver implements Resolve<StrengthJournalContext> {

  constructor(private config: ConfigService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<StrengthJournalContext> {
    const configWithFeatures$ = this.config.config$
      .pipe(
        withLatestFrom(this.config.features$),
        map(([config, features]) => ({ config, features }) as StrengthJournalContext),
        take(1)
      );
    return configWithFeatures$;
  }
}
