import { AbstractControl, ValidatorFn } from '@angular/forms';
import { environment } from '@environments/environment';

export function personalIdentificationNumberValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const pin = control.value;

    if (
      !pin ||
      !environment.validationConstants.common.isAllDigitsRegex.test(pin)
    ) {
      return { pinInvalidChars: true };
    }

    if (
      pin.length !== environment.validationConstants.user.pin.requiredLength
    ) {
      return { pinLength: true };
    }

    return null;
  };
}
