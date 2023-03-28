import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { CarModule } from '../car/car.module';

import { UserRoutingModule } from './user-routing.module';

import { ProfileComponent } from './profile/profile.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { LogoutComponent } from './logout/logout.component';

@NgModule({
  declarations: [
    ProfileComponent,
    LoginComponent,
    RegisterComponent,
    LogoutComponent,
  ],
  imports: [CommonModule, SharedModule, CarModule, UserRoutingModule],
})
export class UserModule {}
