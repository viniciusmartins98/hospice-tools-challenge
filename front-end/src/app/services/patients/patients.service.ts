import { inject, Injectable } from '@angular/core';
import consts from '../../../consts';
import IPatientFilter from './interfaces/patient-filter';
import IPatient from './interfaces/patient';
import { HttpClient } from '@angular/common/http';
import IPaginatedResponse from '../../models/paginated-response';
import IPatientRequest from './interfaces/patient-request';
import IPatientStatistics from './interfaces/patient-statistics';

@Injectable({
  providedIn: 'root',
})
export class PatientsService {
  private readonly _baseUrl = consts.API_URL;
  private readonly http = inject(HttpClient);

  listPatients(filter: IPatientFilter) {
    const params = new URLSearchParams({
      page: filter.page.toString(),
      pageSize: filter.pageSize.toString(),
      'filter.patientName': filter.patientName,
    });

    return this.http.get<IPaginatedResponse<IPatient>>(
      `${this._baseUrl}/patients?${params.toString()}`,
    );
  }

  getPatientStastistics() {
    return this.http.get<IPatientStatistics>(`${this._baseUrl}/patients/statistics`);
  }

  addPatient(patient: IPatientRequest) {
    return this.http.post(`${this._baseUrl}/patients`, patient);
  }

  updatePatient(patientId: string, request: IPatientRequest) {
    return this.http.put(`${this._baseUrl}/patients/${patientId}`, request);
  }

  deletePatient(patientId: string) {
    return this.http.delete(`${this._baseUrl}/patients/${patientId}`);
  }
}
