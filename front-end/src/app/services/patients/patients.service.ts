import { inject, Injectable } from '@angular/core';
import consts from '../../../consts';
import IPatientFilter from './interfaces/patient-filter';
import IPatient from './interfaces/patient';
import { HttpClient } from '@angular/common/http';
import ICreatePatient from './interfaces/create-patient';
import IPaginatedResponse from '../../models/paginated-response';

@Injectable({
  providedIn: 'root',
})
export class PatientsService {
  private readonly _baseUrl = consts.API_URL;
  private readonly http = inject(HttpClient);

  listPatients(filter: IPatientFilter) {
    return this.http.get<IPaginatedResponse<IPatient>>(`${this._baseUrl}/patients`, {
      params: { ...filter } as Record<string, string | number | boolean>
    });
  }

  addPatient(patient: ICreatePatient) {
    return this.http.post(`${this._baseUrl}/patients`, patient);
  }
}
