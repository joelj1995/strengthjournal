import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Country } from 'src/app/model/country';
import { ProfileSettings } from 'src/app/model/profile-settings';
import { ProfileService } from 'src/app/services/profile.service';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  settings: ProfileSettings | null = null;
  countryList: Country[] | null = null;

  settingsForm = new FormGroup({
    preferredWeightUnit: new FormControl(''),
    consentCEM: new FormControl(''),
    countryCode: new FormControl('')
  });

  constructor(private profile: ProfileService, private toast: ToastService) { }

  ngOnInit(): void {
    this.profile.getCountries().subscribe(countries => {
      this.countryList = countries;
    });
    this.profile.getSettings().subscribe(settings => {
      this.settings = settings;
      this.settingsForm.setValue({
        'preferredWeightUnit': settings.preferredWeightUnit,
        'consentCEM': settings.consentCEM,
        'countryCode': settings.countryCode
      });
    });
  }

  onSubmit() {
    this.profile.updateSettings({
      preferredWeightUnit: this.settingsForm.value.preferredWeightUnit,
      consentCEM: this.settingsForm.value.consentCEM == true,
      countryCode: this.settingsForm.value.countryCode
    }).subscribe(() => {
      this.toast.setToast({ message: 'Profile updated', domClass: 'bg-success text-light' });
    });
  }

  resetPassword() {
    this.profile.resetPasword().subscribe(() => {
      this.toast.setToast({ message: 'Password reset sent', domClass: 'bg-success text-light' });
    });
  }

}
