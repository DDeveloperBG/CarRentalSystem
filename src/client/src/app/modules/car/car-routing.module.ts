import { RouterModule, Routes } from '@angular/router';

import { AddRentRequestComponent } from './add-rent-request/add-rent-request.component';

import { AuthGuard } from '@guards/auth.guard';

const routes: Routes = [
  {
    path: 'car/addRentRequest',
    pathMatch: 'full',
    component: AddRentRequestComponent,
    canActivate: [AuthGuard],
  },
];

export const CarRoutingModule = RouterModule.forChild(routes);
