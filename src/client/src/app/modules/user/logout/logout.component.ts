import { Component } from '@angular/core';

import { Router } from '@angular/router';
import { UserService } from '@services/user.service';

@Component({
  selector: 'user-logout',
  template: '<span></span>',
})
export class LogoutComponent {
  constructor(router: Router, userService: UserService) {
    userService.logout();
    router.navigate(['/']);
  }
}
