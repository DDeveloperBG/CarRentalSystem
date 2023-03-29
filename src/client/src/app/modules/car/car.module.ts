import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';

import { CarRoutingModule } from './car-routing.module';

import { AllAdvertisementsComponent } from './all-advertisements/all-advertisements.component';
import { FilteredAdvertisementsComponent } from './filtered-advertisements/filtered-advertisements.component';
import { AddRentRequestComponent } from './add-rent-request/add-rent-request.component';
import { AllUserRentingRequestsComponent } from './all-user-renting-requests/all-user-renting-requests.component';
import { UserRequestsComponent } from './user-requests/user-requests.component';

@NgModule({
  declarations: [
    AllAdvertisementsComponent,
    FilteredAdvertisementsComponent,
    AddRentRequestComponent,
    AllUserRentingRequestsComponent,
    UserRequestsComponent,
  ],
  imports: [CommonModule, SharedModule, CarRoutingModule],
  exports: [FilteredAdvertisementsComponent, AllUserRentingRequestsComponent],
})
export class CarModule {}
