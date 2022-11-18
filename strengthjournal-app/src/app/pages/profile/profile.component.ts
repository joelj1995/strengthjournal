import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
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

  settingsForm = new FormGroup({
    preferredWeightUnit: new FormControl(''),
    consentCEM: new FormControl('')
  });

  constructor(private profile: ProfileService, private toast: ToastService) { }

  ngOnInit(): void {
    this.profile.getSettings().subscribe(settings => {
      this.settings = settings;
      this.settingsForm.setValue({
        'preferredWeightUnit': settings.preferredWeightUnit,
        'consentCEM': settings.consentCEM
      });
    });
  }

  onSubmit() {
    console.log(this.settingsForm.value.consentCEM);
    this.profile.updateSettings({
      preferredWeightUnit: this.settingsForm.value.preferredWeightUnit,
      consentCEM: this.settingsForm.value.consentCEM == true
    }).subscribe(() => {
      this.toast.setToast({ message: 'Profile updated', domClass: 'bg-success text-light' });
    });
  }

}
