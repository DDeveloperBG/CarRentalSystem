import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { IOneOfGetAllPickupLocations } from '@data/pickupLocation/oneOfGetAllPickupLocations.interface';
import { ICarAdDetails } from '@data/car/carAdDetails.interface';
import { AddCarAdRentRequest } from '@data/car/addCarAdRentRequest';

import { CarService } from '@services/car.service';
import { PickupLocationService } from '@services/pickupLocation.service';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'car-add-rent-request',
  templateUrl: './add-rent-request.component.html',
  styleUrls: ['./add-rent-request.component.scss'],
})
export class AddRentRequestComponent implements OnInit {
  carAdvertisementId!: string;
  fromDate!: string;
  toDate!: string;
  carAdDetails?: ICarAdDetails;
  rentPrice?: number;
  pickupLocations: IOneOfGetAllPickupLocations[] = [];

  serverError?: string;

  requestFormGroup: FormGroup;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private pickupLocationService: PickupLocationService,
    private carService: CarService,
    private formBuilder: FormBuilder
  ) {
    this.pickupLocationService.getAll().subscribe((response) => {
      if (response.isSuccessful) {
        this.pickupLocations = response.data;
      }
    });

    this.requestFormGroup = this.formBuilder.group({
      pickupLocation: new FormControl(),
    });
  }

  ngOnInit() {
    this.route.queryParams.subscribe((params) => {
      this.carAdvertisementId = params['carAdvertisementId'];
      this.fromDate = params['fromDate'];
      this.toDate = params['toDate'];

      this.setCarAdDetails();
    });
  }

  addRentingRequest(): void {
    const pickupLocationControl =
      this.requestFormGroup.controls['pickupLocation'];
    if (!pickupLocationControl.valid) {
      pickupLocationControl.markAsTouched();
      return;
    }

    var data = new AddCarAdRentRequest(
      new Date(this.fromDate).toISOString(),
      new Date(this.toDate).toISOString(),
      pickupLocationControl.value,
      this.carAdvertisementId
    );

    this.carService.addRentingRequest(data).subscribe((response) => {
      if (response.isSuccessful) {
        this.router.navigate(['/']);
        return;
      }

      this.serverError = response.message;
    });
  }

  private setCarAdDetails(): void {
    this.carService
      .getOneAdDetails(this.carAdvertisementId)
      .subscribe((response) => {
        if (response.isSuccessful) {
          this.carAdDetails = response.data;
          this.rentPrice =
            this.carAdDetails.rentPricePerDay *
            this.getTwoDatesDifference(this.fromDate, this.toDate);
          return;
        }

        this.serverError = response.message;
      });
  }

  private getTwoDatesDifference(from: string, to: string): number {
    const date1 = new Date(from);
    const date2 = new Date(to);
    const timeDiff = Math.abs(date2.getTime() - date1.getTime());
    const diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
    return diffDays + 1;
  }
}
