import {
  AbstractControl,
  AsyncValidatorFn,
  ValidationErrors,
} from '@angular/forms';

import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '@environments/environment';
import { UserService } from '@services/user.service';
import { IRequestResultModel } from '@data/requestResultModel.interface';

export function usernameValidator(userService: UserService): AsyncValidatorFn {
  return (control: AbstractControl): Observable<ValidationErrors | null> => {
    const username = control.value;

    if (
      !username ||
      username.length <
        environment.validationConstants.user.username.minLength ||
      username.length > environment.validationConstants.user.username.maxLength
    ) {
      return of({ usernameLength: true });
    }

    return userService.usernameExists(username).pipe(
      map((response: IRequestResultModel<boolean>) => {
        return response.data ? { usernameExists: true } : null;
      })
    );
  };
}
