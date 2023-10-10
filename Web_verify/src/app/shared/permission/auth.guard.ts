import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot, CanActivate,
  CanActivateChild,
  Router, RouterStateSnapshot
} from '@angular/router';
import { tokenValid } from '../utils/utils.function';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate, CanActivateChild {
  constructor(private router: Router) { }


  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (tokenValid()) {
      return true;
    }
    this.router.navigate(['/auth/login'], { queryParams: { returnUrl: state.url } });
    return false;
  }

  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    return this.canActivate(route, state);
  }

}
