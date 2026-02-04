import { effect, inject, Injectable, signal, WritableSignal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import IAuthRequest from './interfaces/auth-request';
import IAuthResponse from './interfaces/auth-response';
import IUser from './interfaces/user';
import consts from '../../../consts';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly baseUrl = consts.API_URL;
  private readonly http = inject(HttpClient);
  private readonly router = inject(Router);

  authenticatedUser = signal<IUser | null>(null);

  constructor() {
    var isAuthenticated = sessionStorage.getItem(consts.ACCESS_TOKEN_KEY);
    if (isAuthenticated) {
      this.http.get<IUser>(`${this.baseUrl}/users/auth`).subscribe((user) => {
        this.authenticatedUser.set(user);
      });
    }

    effect(() => {
      if (!this.authenticatedUser()) {
        this.router.navigate(['/login']);
      }
    });
  }

  login(credentials: IAuthRequest): Observable<IAuthResponse> {
    return this.http
      .post<IAuthResponse>(`${this.baseUrl}/auth`, credentials)
      .pipe(
        tap((response) => {
          sessionStorage.setItem(consts.ACCESS_TOKEN_KEY, response.accessToken);
        }),
        tap(() => {
          this.http.get<IUser>(`${this.baseUrl}/users/auth`).subscribe((user) => {
            this.authenticatedUser.set(user);
          });
        })
      );
  }

  logout(): void {
    sessionStorage.removeItem(consts.ACCESS_TOKEN_KEY);
    this.authenticatedUser.set(null);
  }
}
