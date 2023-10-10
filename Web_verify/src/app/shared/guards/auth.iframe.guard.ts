import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot} from '@angular/router';
import {Observable} from "rxjs";
import {AuthIframeService} from "../services/auth-iframe.service";


@Injectable({
  providedIn: 'root',
})
export class AuthIframeGuard implements CanActivate {
  constructor(private authIframeService: AuthIframeService) {
  }


  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    return this.authIframeService.token;
  }
}
