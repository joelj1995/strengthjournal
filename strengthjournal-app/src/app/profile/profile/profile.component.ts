import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
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

  settings!: ProfileSettings;
  countryList!: Country[];

  settingsForm = new FormGroup({
    preferredWeightUnit: new FormControl(''),
    consentCEM: new FormControl(''),
    countryCode: new FormControl('')
  });

  emailFormControl = new FormControl(localStorage.getItem('app_username'));

  constructor(
    private profile: ProfileService, 
    private toast: ToastService, 
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.settings = this.route.snapshot.data['profile'].settings;
    this.countryList = this.route.snapshot.data['profile'].countryList;
    this.settingsForm.setValue({
      'preferredWeightUnit': this.settings.preferredWeightUnit,
      'consentCEM': this.settings.consentCEM,
      'countryCode': this.settings.countryCode
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

  updateEmail() {
    const newEmail = this.emailFormControl.value;
    this.profile.updateEmail(newEmail).subscribe(() => {
      this.toast.setToast({ message: 'Email updated successfully', domClass: 'bg-success text-light' });
      localStorage.setItem('app_username', newEmail);
      location.reload();
    });
  }

}
