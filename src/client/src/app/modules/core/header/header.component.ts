import { Component, OnInit, OnDestroy } from '@angular/core';
import { UserService } from '@services/user.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit, OnDestroy {
  isLoggedIn: boolean = false;
  username: string = '';
  loginSubscription?: Subscription;
  logoutSubscription?: Subscription;

  constructor(private userService: UserService) {}

  ngOnInit() {
    this.isLoggedIn = this.userService.isLoggedIn;
    if (this.isLoggedIn) {
      this.username = this.userService.getUsername();
    }

    this.loginSubscription = this.userService.loginEvent.subscribe(() => {
      this.username = this.userService.getUsername();
      this.isLoggedIn = true;
    });

    this.logoutSubscription = this.userService.logoutEvent.subscribe(() => {
      this.isLoggedIn = false;
      this.username = '';
    });
  }

  ngOnDestroy() {
    this.loginSubscription!.unsubscribe();
    this.logoutSubscription!.unsubscribe();
  }
}
