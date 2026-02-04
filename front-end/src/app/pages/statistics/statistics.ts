import { Component, inject, OnInit, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ColorsService } from '../../services/colors/colors.service';
import { PatientsService } from '../../services/patients/patients.service';
import IColor from '../../services/colors/interfaces/color';
import IPatient from '../../services/patients/interfaces/patient';

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

  patients = signal<IPatient[]>([]);
  isLoading = signal(true);

  colorStats = computed<ColorStat[]>(() => {
    const colors = this.colorsService.colors();
    const patients = this.patients();

    return colors.map(color => ({
      color,
      count: patients.filter(p => p.favoriteColor?.id === color.id).length
    })).sort((a, b) => b.count - a.count);
  });

  totalPatients = computed(() => this.patients().length);

  patientsWithColor = computed(() =>
    this.patients().filter(p => p.favoriteColor !== null).length
  );

  patientsWithoutColor = computed(() =>
    this.patients().filter(p => p.favoriteColor === null).length
  );

  ngOnInit() {
    this.loadPatients();
  }

  loadPatients() {
    this.isLoading.set(true);
    this.patientsService.listPatients({
      page: 1,
      pageSize: 1000,
      patientName: ''
    }).subscribe({
      next: (response) => {
        this.patients.set(response.data);
        this.isLoading.set(false);
      },
      error: () => {
        this.isLoading.set(false);
      }
    });
  }
}
