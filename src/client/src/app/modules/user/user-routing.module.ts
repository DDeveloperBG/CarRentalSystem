import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { LogoutComponent } from './logout/logout.component';
import { ProfileComponent } from './profile/profile.component';

import { AuthGuard } from '@guards/auth.guard';

const routes: Routes = [
  {
    path: 'account/login',
    pathMatch: 'full',
    component: LoginComponent,
  },
  {
    path: 'account/register',
    pathMatch: 'full',
    component: RegisterComponent,
  },
  {
    path: 'account/logout',
    pathMatch: 'full',
    component: LogoutComponent,
  },
  {
    path: 'account/profile',
    pathMatch: 'full',
    component: ProfileComponent,
    canActivate: [AuthGuard],
  },
];

export const UserRoutingModule = RouterModule.forChild(routes);
