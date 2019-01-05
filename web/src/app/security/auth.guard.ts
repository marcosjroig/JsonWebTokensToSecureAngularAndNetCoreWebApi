import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { SecurityService } from './security.service';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private securityService: SecurityService, private router: Router) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
     const claimType: string = next.data['claimType'];

    if (this.securityService.securityObject.isAuthenticated &&
        this.securityService.hasClaim(claimType)) {
          return true;
     } else {
      this.router.navigate(['login'],
      {
        // With this information, we can redirect to this URL after the login succed
        queryParams: { returnUrl: state.url}
      });
     }
  }
}
