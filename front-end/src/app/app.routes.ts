import { Routes } from '@angular/router';

import { Login } from './pages/login/login';
import { Home } from './pages/home/home';
import { authGuard } from './guards/auth-guard';
import { loginGuard } from './guards/login-guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full',
  },
  {
    path: 'login',
    component: Login,
    canActivate: [loginGuard],
  },
  {
    path: 'app',
    canActivate: [authGuard],
    children: [{
      path: '',
      component: Home,
    }]
  }
];
