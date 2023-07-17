import { AuthService } from './auth.service';
import { Injectable, inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

@Injectable()
export class PermissionsService {
  constructor(private authService: AuthService, public router: Router) {}

  canActivate() {
    if (!this.authService.currentUser) {
      this.router.navigate(['/register-passenger']);
    }
    return true;
  }
}

export const authGuard: CanActivateFn = (route, state) => {
  return inject(PermissionsService).canActivate();
};
