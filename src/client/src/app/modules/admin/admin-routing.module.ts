import { RouterModule, Routes } from '@angular/router';
import { globalValues } from '@globalValues';

import { AuthGuard } from '@guards/auth.guard';
import { AddAdvertisementComponent } from './add-advertisement/add-advertisement.component';
import { UnconfirmedCarRentRequestsComponent } from './unconfirmed-car-rent-requests/unconfirmed-car-rent-requests.component';

const routes: Routes = [
  {
    path: 'admin/addAdvertisement',
    pathMatch: 'full',
    component: AddAdvertisementComponent,
    canActivate: [AuthGuard],
    data: { expectedRole: globalValues.roles.admin },
  },
  {
    path: 'admin/unconfirmedRentingRequests',
    pathMatch: 'full',
    component: UnconfirmedCarRentRequestsComponent,
    canActivate: [AuthGuard],
    data: { expectedRole: globalValues.roles.admin },
  },
];

export const AdminRoutingModule = RouterModule.forChild(routes);
