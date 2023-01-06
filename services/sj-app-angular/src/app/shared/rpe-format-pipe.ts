import { Pipe, PipeTransform } from "@angular/core";

@Pipe({name: 'rpe'})
export class RPEPipe implements PipeTransform {
  transform(value: number | null): string {
    if (!value) return '';
    const divided = value / 2;
    return divided.toString();
  }
}