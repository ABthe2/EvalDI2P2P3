import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {Application} from "../../models/application";
import {ApiService} from "../../services/api.service";
import {Account} from "../../models/account";
import {MatFormField, MatLabel} from "@angular/material/form-field";
import {MatOption, MatSelect} from "@angular/material/select";
import {MatInput} from "@angular/material/input";
import {MatButton} from "@angular/material/button";
import {CommonModule} from "@angular/common";
import {Router} from '@angular/router';

@Component({
  selector: 'app-add-password',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatLabel,
    MatFormField,
    MatSelect,
    MatOption,
    MatInput,
    MatButton
  ],
  templateUrl: './add-password.component.html',
  styleUrl: './add-password.component.css'
})
export class AddPasswordComponent implements OnInit {
  addPasswordForm: FormGroup;
  applications: any = [];
  applicationTypes = [
    { id: 1, name: 'Grand public' },
    { id: 2, name: 'Professionnelle' }
  ];
  title = 'Ajouter un mot de passe';

  constructor(private fb: FormBuilder, private apiService: ApiService, private router: Router) {
    this.addPasswordForm = this.fb.group({
      name: ['', Validators.required],
      applicationId: ['', Validators.required],
      applicationType: [{ value: '', disabled: true}, Validators.required],
      password: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.fetchApplications();
    this.addPasswordForm.get('applicationId')?.valueChanges.subscribe(applicationId => {
      const selectedApp = this.applications.find((app: Application) => app.idApplication === applicationId);
      if (selectedApp) {
        this.addPasswordForm.get('applicationType')?.setValue(selectedApp.idApplicationType);
      }
    });
  }

  fetchApplications() {
    this.apiService.getApplications().subscribe({
      next: (applications) => {
        this.applications = applications;
      },
      error: (error) => {
        console.error(error);
      }
    });
  }

  onSubmit() {
    if (this.addPasswordForm.valid) {
      const newAccount: Account = {
        idAccount: 0,
        name: this.addPasswordForm.value.name,
        password: this.addPasswordForm.value.password,
        idApplication: this.addPasswordForm.value.applicationId,
        application: this.applications.find((app: Application) => app.idApplication === this.addPasswordForm.value.applicationId) || new Application()
      };

      this.apiService.createPasswordAccount(newAccount).subscribe({
        next: () => {
          console.log('Account added successfully');
          this.router.navigate(['/passwords']);
        },
        error: (error) => {
          console.error(error);
        }
      });
    }
  }
}
