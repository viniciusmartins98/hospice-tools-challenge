import { Component, inject, OnInit, signal, computed, effect } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ColorsService } from '../../services/colors/colors.service';
import { PatientsService } from '../../services/patients/patients.service';
import IColor from '../../services/colors/interfaces/color';
import IPatientStatistics from '../../services/patients/interfaces/patient-statistics';
import IPatientFavoriteColorCountByAgeRange from '../../services/patients/interfaces/patient-favorite-color-count-by-age-range';
import { GenderEnum } from '../../services/patients/enums/gender.enum';
import IPatientCountByColorAndGender from '../../services/patients/interfaces/patient-count-by-color-and-gender';

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
  totalMale = computed(() => this.patientStatistics()?.maleCount);
  totalFemale = computed(() => this.patientStatistics()?.femaleCount);
  patientsWithColor = computed(() => this.patientStatistics()?.patientsWithColorCount);
  patientsWithoutColor = computed(() => this.patientStatistics()?.patientsWithNoColorsCount);
  patientsByColorAndGender = computed(
    () => this.patientStatistics()?.patientsCountByColorAndGender,
  );

  favoriteColorPatientsCountByAgeRange = computed(() => {
    const dicColorAgeRange = new Map<number, IPatientFavoriteColorCountByAgeRange[]>();

    const favoriteColorPatientsCountByAgeRange =
      this.patientStatistics()?.favoriteColorPatientsCountByAgeRange;
    favoriteColorPatientsCountByAgeRange?.map((item) => {
      const existingItems = dicColorAgeRange.get(item.ageRange.from) ?? [];
      existingItems.push(item);
      dicColorAgeRange.set(item.ageRange.from, [...existingItems]);
    });
    return dicColorAgeRange;
  });

  favoriteColorPatientsCountByGender = computed(() => {
    const dicGenderColors = new Map<GenderEnum, IPatientCountByColorAndGender[]>();
    const patientsCountByColorAndGender =
      this.patientStatistics()?.patientsCountByColorAndGender;

    patientsCountByColorAndGender?.map((item) => {
      const existingItems = dicGenderColors.get(item.gender) ?? [];
      existingItems.push(item);
      dicGenderColors.set(item.gender, [...existingItems]);
    });

    return dicGenderColors;
  });

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
