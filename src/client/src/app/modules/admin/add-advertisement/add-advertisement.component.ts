import { Component, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { globalValues } from '@globalValues';
import { CarService } from '@services/car.service';

@Component({
  selector: 'admin-add-advertisement',
  templateUrl: './add-advertisement.component.html',
  styleUrls: ['./add-advertisement.component.scss'],
})
export class AddAdvertisementComponent {
  files: File[] = [];

  defaultTransmissionType: number = globalValues.car.defaultTransmissionType;
  transmissionTypes: string[] = globalValues.car.transmissionTypes;

  carInfoFormGroup: FormGroup;

  @ViewChild('fileUpload', { static: false })
  fileUpload?: ElementRef;

  serverError?: string;

  constructor(
    private formBuilder: FormBuilder,
    private carService: CarService,
    private router: Router
  ) {
    this.carInfoFormGroup = this.formBuilder.group({
      carBrand: [''],
      carModel: [''],
      carCreationYear: [
        '',
        [Validators.min(1886), Validators.max(new Date().getFullYear())],
      ],
      numberPassengerSeats: [
        globalValues.car.defaultPassengerSeats,
        Validators.min(1),
      ],
      description: [''],
      rentPricePerDay: ['', Validators.min(1)],
      transmissionType: [this.defaultTransmissionType],
    });
  }

  onUploadClick(event: Event) {
    event.preventDefault();
    this.fileUpload?.nativeElement.click();
  }

  onFileSelected(event: Event) {
    event.preventDefault();

    const fileList = (event.target as HTMLInputElement).files;
    if (fileList) {
      Array.from(fileList).forEach((x) => this.files.push(x));
    }
  }

  onSubmit() {
    if (!this.carInfoFormGroup.valid) {
      return;
    }

    const formData = new FormData();

    const controlNames = Object.keys(this.carInfoFormGroup.controls);
    controlNames.forEach((x) => {
      formData.append(x, this.carInfoFormGroup.get(x)?.value);
    });

    for (const file of this.files) {
      formData.append('imageFiles', file);
    }

    this.carService.addAdvertisement(formData).subscribe((requestResult) => {
      if (requestResult.isSuccessful) {
        this.router.navigate(['/']);
        return;
      }

      this.serverError = requestResult.message;
    });
  }
}
