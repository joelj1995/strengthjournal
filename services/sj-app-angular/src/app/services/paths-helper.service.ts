import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PathsHelperService {

  constructor() { }

  getBasePath(url: string): string {
    const optionalSeparatorIdx = url.indexOf(';');
    const substringEnd = optionalSeparatorIdx == -1 ? url.length : optionalSeparatorIdx;
    return url.substring(0, substringEnd);
  }

  baseUrlChanged(url1: string, url2: string): boolean {
    const baseUrl1 = this.getBasePath(url1);
    const baseUrl2 = this.getBasePath(url2);
    return baseUrl1 != baseUrl2;
  }

}
