import { Component, inject, OnInit, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ColorsService } from '../../services/colors/colors.service';
import { PatientsService } from '../../services/patients/patients.service';
import IColor from '../../services/colors/interfaces/color';
import IPatient from '../../services/patients/interfaces/patient';
import IPatientStatistics from '../../services/patients/interfaces/patient-statistics';

interface ColorStat {
  color: IColor;
  count: number;
}

@Component({
  selector: 'app-statistics',
  imports: [CommonModule, MatCardModule, MatProgressSpinnerModule],
  templateUrl: './statistics.html',
  styleUrl: './statistics.scss',
})
export class Statistics implements OnInit {
  private readonly colorsService = inject(ColorsService);
  private readonly patientsService = inject(PatientsService);

  patientStatistics = signal<IPatientStatistics | null>(null);
  isLoading = signal(true);

  colorStats = computed<ColorStat[]>(() => {
    const colors = this.colorsService.colors();
    const patients = this.patientStatistics()?.patientsCountByColor ?? [];

    return colors
      .map((color) => ({
        color,
        count: patients.find((p) => p.color.id === color.id)?.patientsCount ?? 0,
      }))
      .sort((a, b) => b.count - a.count);
  });

  totalPatients = computed(() => this.patientStatistics()?.patientsCount);
  patientsWithColor = computed(() => this.patientStatistics()?.patientsWithColorCount);
  patientsWithoutColor = computed(() => this.patientStatistics()?.patientsWithNoColorsCount);
  //TODO: complete
  favoriteColorPatientsCountByAgeRange = computed(
    () => this.patientStatistics()?.favoriteColorPatientsCountByAgeRange,
  );

  ngOnInit() {
    this._loadPatients();
  }

  private _loadPatients() {
    this.isLoading.set(true);
    this.patientsService.getPatientStastistics().subscribe({
      next: (response) => {
        this.patientStatistics.set(response);
        this.isLoading.set(false);
      },
      error: () => {
        this.isLoading.set(false);
      },
    });
  }
}
