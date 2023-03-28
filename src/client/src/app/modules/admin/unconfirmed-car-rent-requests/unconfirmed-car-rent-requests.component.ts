import { Component, Input, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { IOneOfAllUnconfirmedRentRequests } from '@data/car/oneOfAllUnconfirmedRentRequests.interface';
import { CarService } from '@services/car.service';

@Component({
  selector: 'admin-unconfirmed-car-rent-requests',
  templateUrl: './unconfirmed-car-rent-requests.component.html',
  styleUrls: ['./unconfirmed-car-rent-requests.component.scss'],
})
export class UnconfirmedCarRentRequestsComponent implements OnInit {
  @Input() requests = new MatTableDataSource<IOneOfAllUnconfirmedRentRequests>(
    []
  );
  displayedColumns: string[] = [
    'car',
    'rentingPeriod',
    'price',
    'pickupLocation',
    'star',
  ];
  serverError?: string;

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
