import { AbstractControl, ValidatorFn } from '@angular/forms';
import { environment } from '@environments/environment';

export function passwordValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const value = control.value;

    if (
      !value ||
      value.length < environment.validationConstants.user.password.minLength ||
      value.length > environment.validationConstants.user.password.maxLength
    ) {
      return { passwordLength: true };
    }

    if (environment.validationConstants.user.password.requireDigit) {
      if (
        !environment.validationConstants.user.password.requireDigitRegex.test(
          value
        )
      ) {
        return { passwordRequireDigit: true };
      }
    }

    if (environment.validationConstants.user.password.requireLowercase) {
      if (
        !environment.validationConstants.user.password.requireLowercaseRegex.test(
          value
        )
      ) {
        return { passwordRequireLowercase: true };
      }
    }

    if (environment.validationConstants.user.password.requireUppercase) {
      if (
        !environment.validationConstants.user.password.requireUppercaseRegex.test(
          value
        )
      ) {
        return { passwordRequireUppercase: true };
      }
    }

    if (environment.validationConstants.user.password.requireNonAlphanumeric) {
      if (
        !environment.validationConstants.user.password.requireNonAlphanumericRegex.test(
          value
        )
      ) {
        return { passwordRequireNonAlphanumeric: true };
      }
    }

    return null;
  };
}

