import { Component } from '@angular/core';
import { DatePipe } from '@angular/common';
import { FormGroup, FormControl } from '@angular/forms';

import { AllCarAdsInRangeInput } from '@data/car/allCarAdsInRangeInput';

const today = new Date();
const day = today.getDate();
const month = today.getMonth();
const year = today.getFullYear();

@Component({
  selector: 'car-filtered-advertisements',
  templateUrl: './filtered-advertisements.component.html',
  styleUrls: ['./filtered-advertisements.component.scss'],
  providers: [DatePipe],
})
export class FilteredAdvertisementsComponent {
  dateRangeFormGroup: FormGroup;
  period?: AllCarAdsInRangeInput;

  constructor(private datePipe: DatePipe) {
    this.dateRangeFormGroup = new FormGroup({
      from: new FormControl(new Date(year, month, day + 1)),
      to: new FormControl(new Date(year, month, day + 5)),
    });
  }

  updatePeriod() {
    this.period = new AllCarAdsInRangeInput(
      this.turnDateFormInputToString('from'),
      this.turnDateFormInputToString('to')
    );
  }

  turnDateFormInputToString(inputName: string): string | null {
    const formInput = this.dateRangeFormGroup.controls[inputName].value;
    return this.datePipe.transform(formInput, 'M/d/yyyy');
  }
}
