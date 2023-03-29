import { Component, Input } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { IOneOfAllUserRentingRequests } from '@data/car/oneOfAllUserRentingRequests.interface';

@Component({
  selector: 'car-user-requests',
  templateUrl: './user-requests.component.html',
  styleUrls: ['./user-requests.component.scss'],
})
export class UserRequestsComponent {
  @Input() requests!: MatTableDataSource<IOneOfAllUserRentingRequests>;
  displayedColumns: string[] = [
    'car',
    'rentingPeriod',
    'price',
    'pickupLocation',
  ];
}
