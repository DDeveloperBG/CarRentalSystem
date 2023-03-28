import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';

import { AdminRoutingModule } from './admin-routing.module';

import { UnconfirmedCarRentRequestsComponent } from './unconfirmed-car-rent-requests/unconfirmed-car-rent-requests.component';
import { AddAdvertisementComponent } from './add-advertisement/add-advertisement.component';

@NgModule({
  imports: [CommonModule, SharedModule, AdminRoutingModule],
  declarations: [
    UnconfirmedCarRentRequestsComponent,
    AddAdvertisementComponent,
  ],
})
export class AdminModule {}
