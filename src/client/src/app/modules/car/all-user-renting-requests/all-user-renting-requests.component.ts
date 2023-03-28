import { Component, Input, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { IOneOfAllUserRentingRequests } from '@data/car/oneOfAllUserRentingRequests.interface';
import { CarService } from '@services/car.service';

@Component({
  selector: 'car-all-user-renting-requests',
  templateUrl: './all-user-renting-requests.component.html',
  styleUrls: ['./all-user-renting-requests.component.scss'],
})
export class AllUserRentingRequestsComponent implements OnInit {
  @Input() forThePast!: boolean;

  waitingRequests = new MatTableDataSource<IOneOfAllUserRentingRequests>([]);
  confirmedRequests = new MatTableDataSource<IOneOfAllUserRentingRequests>([]);

  constructor(private carService: CarService) {}

  ngOnInit(): void {
    this.carService
      .getAllUserRentingRequest(this.forThePast)
      .subscribe((response) => {
        if (response.isSuccessful) {
          this.waitingRequests.data = response.data.filter(
            (x) => !x.isConfirmed
          );
          this.confirmedRequests.data = response.data.filter(
            (x) => x.isConfirmed
          );
        }
      });
  }
}
