import { Component } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';

import { UserService } from '@services/user.service';
import userDataValidators from '@validators/user/index';
import { globalValues } from '@globalValues';
import { UserRegisterModel } from '@data/userRegisterModel';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  personalInformationFormGroup: FormGroup;
  userCredentialsFormGroup: FormGroup;
  serverError: string = '';

  constructor(
    private router: Router,
    private userService: UserService,
    private formBuilder: FormBuilder
  ) {
    this.personalInformationFormGroup = this.formBuilder.group({
      forename: ['', userDataValidators.forenameValidator()],
      surname: ['', userDataValidators.surnameValidator()],
      personalIdentificationNumber: [
        '',
        userDataValidators.personalIdentificationNumberValidator(),
      ],
      phoneNumber: ['', userDataValidators.phoneNumberValidator()],
    });

    this.userCredentialsFormGroup = this.formBuilder.group(
      {
        username: new FormControl(
          '',
          [Validators.required],
          [userDataValidators.usernameValidator(this.userService)]
        ),
        email: ['', Validators.email],
        password: ['', userDataValidators.passwordValidator()],
        confirmPassword: [''],
      },
      {
        validator: this.ConfirmPasswordValidator('password', 'confirmPassword'),
      }
    );
  }

  get personalInformationForm() {
    return this.personalInformationFormGroup.controls;
  }

  get userCredentialsForm() {
    return this.userCredentialsFormGroup.controls;
  }

  get errorMessages() {
    return globalValues.errorMessages.user;
  }

  onSubmit() {
    var userData = new UserRegisterModel(
      this.personalInformationForm['forename'].value,
      this.personalInformationForm['surname'].value,
      this.personalInformationForm['personalIdentificationNumber'].value,
      this.personalInformationForm['phoneNumber'].value,
      this.userCredentialsForm['email'].value,
      this.userCredentialsForm['username'].value,
      this.userCredentialsForm['password'].value
    );
    console.log(userData);

    this.userService.register(userData).subscribe((requestResult) => {
      if (requestResult.isSuccessful) {
        this.router.navigate(['/']);
        return;
      }

      this.serverError = requestResult.message;
    });
  }

  ConfirmPasswordValidator(passwordName: string, confirmPasswordName: string) {
    return (formGroup: FormGroup) => {
      const password = formGroup.controls[passwordName].value;
      const confirmPassword = formGroup.controls[confirmPasswordName].value;

      if (password !== confirmPassword) {
        formGroup.controls[confirmPasswordName].setErrors({
          confirmPasswordRequireMatch: true,
        });
      }
    };
  }
}
