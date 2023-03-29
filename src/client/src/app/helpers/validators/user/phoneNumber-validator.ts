import { AbstractControl, ValidatorFn } from '@angular/forms';
import { environment } from '@environments/environment';

export function phoneNumberValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const phoneNumber = control.value;

    if (
      !phoneNumber ||
      !environment.validationConstants.user.phoneNumber.regex.test(phoneNumber)
    ) {
      return { phoneNumberInvalid: true };
    }

    return null;
  };
}
