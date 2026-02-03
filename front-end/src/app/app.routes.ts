import { Routes } from '@angular/router';

import { Login } from './pages/login/login';
import { Home } from './pages/home/home';
import { authGuard } from './guards/auth-guard';
import { loginGuard } from './guards/login-guard';
import { Layout } from './components/layout/layout';
import { Statistics } from './pages/statistics/statistics';

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
    component: Layout,
    pathMatch: 'prefix',
    children: [{
      path: '',
      component: Home,
    }, {
      path: 'statistics',
      component: Statistics,
    }]
  }
];
