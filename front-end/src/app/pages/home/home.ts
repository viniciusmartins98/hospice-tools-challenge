import { Component, computed, effect, inject, model, OnDestroy, OnInit, signal, untracked } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import IPatient from '../../services/patients/interfaces/patient';
import { PatientsService } from '../../services/patients/patients.service';
import { BehaviorSubject, debounceTime, distinctUntilChanged, finalize, Subscription, tap } from 'rxjs';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import IPaginatedResponse from '../../models/paginated-response';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-home',
  imports: [
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    FormsModule,
    NgClass
  ],
  templateUrl: './home.html',
  styleUrl: './home.scss',
})
export class Home implements OnInit, OnDestroy {
  private readonly _patientsService = inject(PatientsService);
  page = signal(1);
  pageSize = signal(5);
  patientName = model('');
  patientFilterName$ = new BehaviorSubject<string>('');

  patientResponse = signal<IPaginatedResponse<IPatient>>({
    data: [],
    totalItens: 0,
    totalPages: 0,
    currentPage: this.page(),
    pageSize: this.pageSize()
  });
  patients = computed(() => this.patientResponse().data);
  loadingPatients = signal(false);
  private _subscriptionsToUnsubscribe: Subscription[] = [];

  columns = ['name', 'gender', 'age', 'favoriteColor', 'actions'];

  constructor() {
    // Every time patientName changes then publish new value to patientFilterName$ which triggers debounced patient filter update
    effect(() => {
      this.patientFilterName$.next(this.patientName().trim());
    })

    // Every time page or pageSize changes triggers patient fetch
    effect(() => {
      if (this.page() <= 0 || this.pageSize() <= 0) {
        return;
      }
      untracked(() => {
        this._fetchPatients();
      })
    })
  }

  onPageChange(event: PageEvent) {
    this.page.set(event.pageIndex + 1);
    this.pageSize.set(event.pageSize);
  }

  private _fetchPatients() {
    this.loadingPatients.set(true);
    this._patientsService.listPatients({
      page: this.page(),
      pageSize: this.pageSize(),
      patientName: this.patientName()
    })
      .pipe(
        finalize(() => {
          this.loadingPatients.set(false);
        })
      )
      .subscribe((response) => {
        this.patientResponse.set(response);
      });
  }

  ngOnInit(): void {
    const subscription = this.patientFilterName$
      .pipe(distinctUntilChanged(), debounceTime(500))
      .subscribe(() => {
        this._fetchPatients();
      });
    this._subscriptionsToUnsubscribe.push(subscription);
  }

  ngOnDestroy(): void {
    this._subscriptionsToUnsubscribe.forEach((subscription) => subscription.unsubscribe());
  }
}
