import { Component, Input, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { IOneOfAllUnconfirmedRentRequests } from '@data/car/oneOfAllUnconfirmedRentRequests.interface';
import { CarService } from '@services/car.service';
import {
  animate,
  state,
  style,
  transition,
  trigger,
} from '@angular/animations';

@Component({
  selector: 'admin-unconfirmed-car-rent-requests',
  templateUrl: './unconfirmed-car-rent-requests.component.html',
  styleUrls: ['./unconfirmed-car-rent-requests.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition(
        'expanded <=> collapsed',
        animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')
      ),
    ]),
  ],
})
export class UnconfirmedCarRentRequestsComponent implements OnInit {
  @Input() requests = new MatTableDataSource<IOneOfAllUnconfirmedRentRequests>(
    []
  );
  columnsToDisplay: string[] = [
    'car',
    'rentingPeriod',
    'price',
    'pickupLocation',
  ];
  columnsToDisplayWithExpand = [...this.columnsToDisplay, 'expand'];
  serverError?: string;
  expandedElement?: IOneOfAllUnconfirmedRentRequests | null;

  constructor(private carService: CarService) {}

  ngOnInit(): void {
    this.carService.getUnconfirmedRentRequestsAsync().subscribe((response) => {
      if (response.isSuccessful) {
        this.requests.data = response.data;
      }
    });
  }

  confirmRequest(id: string) {
    this.carService.confirmRentingRequest(id).subscribe((response) => {
      if (response.isSuccessful) {
        window.location.reload();
        return;
      }

      this.serverError = response.message;
    });
  }
}
