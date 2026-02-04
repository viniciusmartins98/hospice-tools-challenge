import { Component, computed, inject, input, OnInit, output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import IColor from '../../services/colors/interfaces/color';
import IPatient from '../../services/patients/interfaces/patient';
import { GenderEnum } from '../../services/patients/enums/gender.enum';
import IPatientFormOutput from './interfaces/patient-form-output';
import { ColorsService } from '../../services/colors/colors.service';

@Component({
  selector: 'app-patient-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
  ],
  templateUrl: './patient-form.html',
  styleUrl: './patient-form.scss',
})
export class PatientForm implements OnInit {
  private readonly _colorsService = inject(ColorsService);
  patient = input<IPatient | null>(null);
  onSubmit = output<IPatientFormOutput>();

  patientForm = new FormGroup({
    firstName: new FormControl('', [
      Validators.required,
      Validators.minLength(1),
      Validators.maxLength(50),
    ]),
    lastName: new FormControl('', [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(50),
    ]),
    gender: new FormControl<GenderEnum | null>(null, Validators.maxLength(10)),
    age: new FormControl<number | null>(null, [Validators.min(0), Validators.max(150)]),
    favoriteColorId: new FormControl<string | null>(null),
  });

  genderOptions = [
    { value: 'Male', label: 'Male' },
    { value: 'Female', label: 'Female' },
  ];

  colorOptions = computed(() => this._colorsService.colors());

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
      favoriteColorId: patient.favoriteColor?.id || null,
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
        favoriteColorId: patientData.favoriteColorId ?? null,
      });
    } else {
      this.patientForm.markAllAsTouched();
    }
  }
}
