import { Component, input, OnInit, output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import IColor from '../../services/patients/interfaces/color';
import IPatient from '../../services/patients/interfaces/patient';
import { GenderEnum } from '../../services/patients/enums/gender.enum';
import IPatientFormOutput from './interfaces/patient-form-output';
import { MatMenuContent } from "@angular/material/menu";
import { ɵEmptyOutletComponent } from "@angular/router";

@Component({
  selector: 'app-patient-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule
  ],
  templateUrl: './patient-form.html',
  styleUrl: './patient-form.scss',
})
export class PatientForm implements OnInit {
  patient = input<IPatient | null>(null);
  onSubmit = output<IPatientFormOutput>();

  patientForm = new FormGroup({
    firstName: new FormControl('', [Validators.required, Validators.minLength(2)]),
    lastName: new FormControl('', [Validators.required, Validators.minLength(2)]),
    gender: new FormControl<GenderEnum | null>(null),
    age: new FormControl<number | null>(null, [Validators.min(0), Validators.max(150)]),
    favoriteColorId: new FormControl<string | null>(null)
  });

  genderOptions = [
    { value: 'Male', label: 'Male' },
    { value: 'Female', label: 'Female' }
  ];

  // TODO: Get colors from API
  colorOptions: IColor[] = [];

  ngOnInit(): void {
    if (!this.patient()) {
      return;
    }
    this.initializeForm(this.patient()!);
  }

  private initializeForm(patient: IPatient): void {
    this.patientForm.setValue({
      firstName: patient.firstName,
      lastName: patient.lastName,
      gender: patient.gender,
      age: patient.age,
      favoriteColorId: patient.favoriteColor?.id || null
    });
  }

  validateAndSubmit(): void {
    if (this.patientForm.valid) {
      const patientData = this.patientForm.value;
      this.onSubmit.emit({
        patientId: this.patient()?.id,
        age: patientData.age ?? null,
        firstName: patientData.firstName ?? '',
        lastName: patientData.lastName ?? '',
        gender: patientData.gender ?? null,
        favoriteColorId: patientData.favoriteColorId ?? null
      });
    } else {
      this.patientForm.markAllAsTouched();
    }
  }
}
