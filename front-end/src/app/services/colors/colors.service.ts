import { inject, Injectable, signal } from '@angular/core';
import consts from '../../../consts';
import { HttpClient } from '@angular/common/http';
import IColor from './interfaces/color';

@Injectable({
  providedIn: 'root',
})
export class ColorsService {
  private readonly _baseUrl = consts.API_URL;
  private readonly http = inject(HttpClient);

  colors = signal<IColor[]>([]);

  constructor() {
    this.fetchColors();
  }

  fetchColors() {
    this.http.get<IColor[]>(`${this._baseUrl}/colors`).subscribe((colors) => {
      this.colors.set(colors);
    });
  }
}
