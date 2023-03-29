import { AbstractControl, ValidatorFn } from '@angular/forms';
import { environment } from '@environments/environment';

export function forenameValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const forename = control.value;

    if (
      !forename ||
      forename.length <
        environment.validationConstants.user.forename.minLength ||
      forename.length > environment.validationConstants.user.forename.maxLength
    ) {
      return { forenameLength: true };
    }

    if (
      !environment.validationConstants.common.isAllLettersRegex.test(forename)
    ) {
      return { forenameInvalidChars: true };
    }

    return null;
  };
}

export function surnameValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const surname = control.value;

    if (
      !surname ||
      surname.length < environment.validationConstants.user.surname.minLength ||
      surname.length > environment.validationConstants.user.surname.maxLength
    ) {
      return { surnameLength: true };
    }

    if (
      !environment.validationConstants.common.isAllLettersRegex.test(surname)
    ) {
      return { surnameInvalidChars: true };
    }

    return null;
  };
}
