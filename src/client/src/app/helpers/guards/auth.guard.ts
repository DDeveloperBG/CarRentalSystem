import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
} from '@angular/router';
import { UserService } from '@services/user.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private userService: UserService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    if (this.userService.isLoggedIn) {
      const expectedRole = route.data['expectedRole'];

      if (
        expectedRole != undefined &&
        !this.userService.isOfRole(expectedRole)
      ) {
        this.navigateToLogin(state);
        return false;
      }

      return true;
    }

    this.navigateToLogin(state);
    return false;
  }

  private navigateToLogin(state: RouterStateSnapshot): void {
    this.router.navigate(['/account/login'], {
      queryParams: { returnUrl: state.url },
    });
  }
}
