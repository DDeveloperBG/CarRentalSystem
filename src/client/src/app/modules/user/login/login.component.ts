import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { UserService } from '@services/user.service';

import { environment } from '@environments/environment';

import { UserLoginModel } from '@data/userLoginModel';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  userFormGroup: FormGroup;
  serverError: string = '';

  constructor(
    private router: Router,
    private userService: UserService,
    private formBuilder: FormBuilder
  ) {
    this.userFormGroup = this.formBuilder.group({
      username: [
        '',
        Validators.maxLength(
          environment.validationConstants.user.username.maxLength
        ),
      ],
      password: [
        '',
        Validators.maxLength(
          environment.validationConstants.user.password.maxLength
        ),
      ],
    });
  }

  onSubmit() {
    var userData = new UserLoginModel(
      this.userFormGroup.controls['username'].value,
      this.userFormGroup.controls['password'].value
    );

    this.userService.login(userData).subscribe((requestResult) => {
      if (requestResult.isSuccessful) {
        this.router.navigate(['/']);
        return;
      }

      this.serverError = requestResult.message;
    });
  }
}
