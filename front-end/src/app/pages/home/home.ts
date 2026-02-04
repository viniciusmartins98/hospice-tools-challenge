import {
  Component,
  computed,
  effect,
  inject,
  model,
  OnDestroy,
  OnInit,
  signal,
  TemplateRef,
  untracked,
  ViewChild,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import IPatient from '../../services/patients/interfaces/patient';
import { PatientsService } from '../../services/patients/patients.service';
import {
  BehaviorSubject,
  debounceTime,
  distinctUntilChanged,
  finalize,
  Subscription,
  tap,
} from 'rxjs';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import IPaginatedResponse from '../../services/patients/interfaces/paginated-response';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { NgClass } from '@angular/common';
import { PatientForm } from '../../components/patient-form/patient-form';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import IPatientFormOutput from '../../components/patient-form/interfaces/patient-form-output';
import IPatientFilter from '../../services/patients/interfaces/patient-filter';

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
    MatDialogModule,
    FormsModule,
    PatientForm,
    NgClass,
  ],
  templateUrl: './home.html',
  styleUrl: './home.scss',
})
export class Home implements OnInit, OnDestroy {
  private readonly _patientsService = inject(PatientsService);
  private readonly _matDialog = inject(MatDialog);
  editingPatient = signal<IPatient | null>(null);
  page = signal(1);
  pageSize = signal(5);
  patientName = model('');
  patientFilter$ = new BehaviorSubject<IPatientFilter>({
    page: 1,
    pageSize: 5,
    patientName: '',
  });

  patientResponse = signal<IPaginatedResponse<IPatient>>({
    data: [],
    totalItens: 0,
    totalPages: 0,
    currentPage: this.page(),
    pageSize: this.pageSize(),
  });
  patients = computed(() => this.patientResponse().data);
  loadingPatients = signal(false);
  persisting = signal(false);

  @ViewChild('patientFormDialog')
  private readonly _patientFormDialogRef!: TemplateRef<any>;

  @ViewChild('confirmDeletionDialog')
  private readonly _confirmDeletionDialogRef!: TemplateRef<any>;

  private _subscriptionsToUnsubscribe: Subscription[] = [];

  columns = ['name', 'gender', 'age', 'favoriteColor', 'actions'];

  constructor() {
    // Every time filter changes then publish new value to patientFilter$ which triggers debounced patient filter update
    effect(() => {
      this.patientFilter$.next({
        page: this.page(),
        pageSize: this.pageSize(),
        patientName: this.patientName(),
      });
    });

    // Every time patientNameChanges reset page
    effect(() => {
      const _ = this.patientName();
      this.page.set(1);
    });
  }

  ngOnInit(): void {
    const subscription = this.patientFilter$
      .pipe(distinctUntilChanged(), debounceTime(500))
      .subscribe(() => {
        this._fetchPatients();
      });
    this._subscriptionsToUnsubscribe.push(subscription);
  }

  private _fetchPatients() {
    this.loadingPatients.set(true);
    this._patientsService
      .listPatients({
        page: this.page(),
        pageSize: this.pageSize(),
        patientName: this.patientName(),
      })
      .pipe(
        finalize(() => {
          this.loadingPatients.set(false);
        }),
      )
      .subscribe((response) => {
        this.patientResponse.set(response);
      });
  }

  onPageChange(event: PageEvent) {
    this.page.set(event.pageIndex + 1);
    this.pageSize.set(event.pageSize);
  }

  openPatientFormDialog(patient?: IPatient) {
    this.editingPatient.set(patient ?? null);
    this._matDialog.open(this._patientFormDialogRef, {
      width: '100%',
      maxWidth: '550px',
    });
  }

  openDeletePatientDialog(patientId: string) {
    this._matDialog.open(this._confirmDeletionDialogRef, {
      width: '100%',
      maxWidth: '550px',
      data: {
        patientId,
      },
    });
  }

  addOrUpdatePatient(patient: IPatientFormOutput) {
    this.persisting.set(true);
    if (this.editingPatient()) {
      this._patientsService.updatePatient(this.editingPatient()!.id, patient).subscribe({
        next: () => {
          this.persisting.set(false);
          this._matDialog.closeAll();
          this._fetchPatients();
        },
        error: (error) => {
          this.persisting.set(false);
          console.error(error);
        },
      });
    } else {
      this._patientsService.addPatient(patient).subscribe({
        next: () => {
          this.persisting.set(false);
          this._matDialog.closeAll();
          this._fetchPatients();
        },
        error: (error) => {
          this.persisting.set(false);
          console.error(error);
        },
      });
    }
  }

  deletePatient(patientId: string) {
    this.persisting.set(true);
    this._patientsService.deletePatient(patientId).subscribe(() => {
      this.persisting.set(false);
      this._matDialog.closeAll();
      this._fetchPatients();
    });
  }

  ngOnDestroy(): void {
    this._subscriptionsToUnsubscribe.forEach((subscription) => subscription.unsubscribe());
  }
}
