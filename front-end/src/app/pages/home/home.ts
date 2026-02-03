import { Component, computed, inject } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';

@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.html',
  styleUrl: './home.scss',
})
export class Home {
  private _authService = inject(AuthService);
  user = computed(() => this._authService.authenticatedUser());
}
