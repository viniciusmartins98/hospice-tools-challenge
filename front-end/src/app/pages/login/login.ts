import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';

import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Router, RouterModule } from '@angular/router';
import ILoginForm from './interfaces/login-form';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '../../services/auth/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './login.html',
  styleUrl: './login.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Login {
  loginForm = new FormGroup<ILoginForm>({
    username: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
    password: new FormControl<string>('', { nonNullable: true, validators: [Validators.required, Validators.minLength(5)] }),
  });
  isLoading = signal(false);
  errorMessage = signal<string | null>(null);
  router = inject(Router);
  authService = inject(AuthService);

  onSubmit(): void {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.isLoading.set(true);
    this.errorMessage.set(null);

    this.authService.login({
      username: this.loginForm.value.username!,
      password: this.loginForm.value.password!
    }).subscribe({
      next: () => {
        this.isLoading.set(false);
        this.router.navigate(['/app']);
      },
      error: () => {
        this.isLoading.set(false);
        this.errorMessage.set("Invalid credentials");
      }
    });
  }
}
