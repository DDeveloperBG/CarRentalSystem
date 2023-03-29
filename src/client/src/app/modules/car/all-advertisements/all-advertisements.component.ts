import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { IAllCarAds } from '@data/car/allCarAds.interface';
import { AllCarAdsInRangeInput } from '@data/car/allCarAdsInRangeInput';
import { CarService } from '@services/car.service';
import { UserService } from '@services/user.service';
import { globalValues } from '@globalValues';

@Component({
  selector: 'car-all-advertisements',
  templateUrl: './all-advertisements.component.html',
  styleUrls: ['./all-advertisements.component.scss'],
})
export class AllAdvertisementsComponent implements OnChanges {
  @Input() period?: AllCarAdsInRangeInput;

  allCars?: IAllCarAds;
  serverError?: string;

  constructor(
    private carService: CarService,
    private userService: UserService,
    private router: Router
  ) {}

  get isAdmin(): boolean {
    return this.userService.isOfRole(globalValues.roles.admin);
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['period']) {
      this.carService.getAllAds(this.period).subscribe((response) => {
        if (response.isSuccessful) {
          this.allCars = response.data;
        }

        this.serverError = response.message;
      });
    }
  }

  requestToReserveCar(event: Event) {
    const carAdId: string = this.getCarIdFromEventTarget(event);

    this.router.navigate(['/car/addRentRequest'], {
      queryParams: {
        carAdvertisementId: carAdId,
        fromDate: this.period?.fromDate,
        toDate: this.period?.toDate,
      },
    });
  }

  deleteCar(event: Event) {
    const carAdId: string = this.getCarIdFromEventTarget(event);

    this.carService.deleteAdvertisement(carAdId).subscribe((response) => {
      if (response.isSuccessful) {
        window.location.reload();
        return;
      }

      this.serverError = response.message;
    });
  }

  private getCarIdFromEventTarget(event: Event): string {
    return (event.target as Element).closest('mat-card')!.id;
  }
}
