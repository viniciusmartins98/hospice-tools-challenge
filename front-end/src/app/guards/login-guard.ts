import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import consts from '../../consts';

export const loginGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const token = sessionStorage.getItem(consts.ACCESS_TOKEN_KEY);
  if (token) {
    router.navigate(['/app']);
    return false;
  }
  return true;
};
