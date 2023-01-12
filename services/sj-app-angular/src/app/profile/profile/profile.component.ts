import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { StrengthJournalContext } from 'src/app/core/strength-journal-context';
import { IamService } from 'src/app/iam/iam.service';
import { Country } from 'src/app/model/country';
import { ProfileSettings } from 'src/app/model/profile-settings';
import { ProfileService } from 'src/app/services/profile.service';
import { ToastService } from 'src/app/services/toast.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit, OnDestroy {

  context: StrengthJournalContext = this.route.snapshot.data['context'];

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
    private route: ActivatedRoute,
    private iam: IamService) { }

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }

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
    this.subs.sink = this.profile.updateSettings({
      preferredWeightUnit: this.settingsForm.value.preferredWeightUnit,
      consentCEM: this.settingsForm.value.consentCEM == true,
      countryCode: this.settingsForm.value.countryCode
    }).subscribe(() => {
      this.toast.setToast({ message: 'Profile updated', domClass: 'bg-success text-light' });
    });
  }

  resetPassword() {
    this.subs.sink = this.iam.resetPassword(this.context.config.userName).subscribe(() => {
      this.toast.setToast({ message: 'Password reset sent', domClass: 'bg-success text-light' });
    });
  }

  updateEmail() {
    const newEmail = this.emailFormControl.value;
    this.subs.sink = this.profile.updateEmail(newEmail).subscribe(() => {
      this.toast.setToast({ message: 'Email updated successfully', domClass: 'bg-success text-light' });
      localStorage.setItem('app_username', newEmail);
      location.reload();
    });
  }

  private subs = new SubSink();

}
