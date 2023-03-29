import { Component, OnInit, OnDestroy } from '@angular/core';
import { UserService } from '@services/user.service';
import { Subscription } from 'rxjs';

import { globalValues } from '@globalValues';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit, OnDestroy {
  isLoggedIn: boolean = false;
  isAdmin: boolean = false;
  username: string = '';
  loginSubscription?: Subscription;
  logoutSubscription?: Subscription;

  constructor(private userService: UserService) {}

  ngOnInit() {
    this.isLoggedIn = this.userService.isLoggedIn;
    if (this.isLoggedIn) {
      this.updateUserValues('set');
    }

    this.loginSubscription = this.userService.loginEvent.subscribe(
      this.updateUserValues.bind(this, 'set')
    );

    this.logoutSubscription = this.userService.logoutEvent.subscribe(
      this.updateUserValues.bind(this, 'nullify')
    );
  }

  ngOnDestroy() {
    this.loginSubscription!.unsubscribe();
    this.logoutSubscription!.unsubscribe();
  }

  private updateUserValues(status: string): void {
    switch (status) {
      case 'set':
        this.isLoggedIn = true;
        this.username = this.userService.getUsername();
        this.isAdmin = this.userService.isOfRole(globalValues.roles.admin);
        break;

      case 'nullify':
        this.isLoggedIn = false;
        this.isAdmin = false;
        this.username = '';
        break;
    }
  }
}
