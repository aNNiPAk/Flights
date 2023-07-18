import { AuthService } from './auth.service';
import { Injectable, inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class PermissionsService {
  constructor(private authService: AuthService, public router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: any) {
    if (!this.authService.currentUser) {
      this.router.navigate([
        '/register-passenger',
        { requestedUrl: state.url },
      ]);
    }
    return true;
  }
}

export const authGuard: CanActivateFn = (route, state) => {
  return inject(PermissionsService).canActivate(route, state);
};
