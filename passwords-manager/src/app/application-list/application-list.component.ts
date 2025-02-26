import {Component, OnInit} from '@angular/core';
import {MatCell, MatColumnDef, MatHeaderCell, MatTable, MatTableModule} from "@angular/material/table";
import {MatFormField, MatFormFieldModule, MatLabel} from "@angular/material/form-field";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {ApiService} from "../../services/api.service";
import {Application} from "../../models/application";
import {CommonModule} from "@angular/common";
import {MatInputModule} from "@angular/material/input";
import {MatButtonModule} from "@angular/material/button";
import {MatOption} from "@angular/material/autocomplete";
import {MatSelect} from "@angular/material/select";

@Component({
  selector: 'app-application-list',
  standalone: true,
  imports: [
    CommonModule,
    MatTable,
    MatLabel,
    MatColumnDef,
    MatHeaderCell,
    MatCell,
    MatFormField,
    ReactiveFormsModule,
    MatTableModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatOption,
    MatSelect
  ],
  templateUrl: './application-list.component.html',
  styleUrl: './application-list.component.css'
})
export class ApplicationListComponent implements OnInit {
  applications: any = [];
  displayedColumns: string[] = ['id', 'name', 'type'];
  addApplicationForm: FormGroup;
  applicationTypes = [
    { idApplicationType: 1, name: 'Grand public' },
    { idApplicationType: 2, name: 'Professionnelle' }
  ];
  title = 'Liste des applications';

  constructor(private fb: FormBuilder, private apiService: ApiService) {
    this.addApplicationForm = this.fb.group({
      name: ['', Validators.required],
      idApplicationType: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.fetchApplications();
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
    if (this.addApplicationForm.valid) {
      const newApplication: Application = {
        idApplication: 0,
        name: this.addApplicationForm.value.name,
        idApplicationType: this.addApplicationForm.value.idApplicationType,
        applicationType: this.applicationTypes.find(type => type.idApplicationType === this.addApplicationForm.value.idApplicationType) || { idApplicationType: 0, name: '' }
      };

      this.apiService.createApplication(newApplication).subscribe({
        next: () => {
          this.fetchApplications();
          this.addApplicationForm.reset();
        },
        error: (error) => {
          console.error(error);
        }
      });
    }
  }
}
