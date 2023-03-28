import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { StorageService } from './storage.service';

import { extractResponse } from '@utils/response.utils';

import { UserLoginModel } from '@data/user/userLoginModel';
import { UserRegisterModel } from '@data/user/userRegisterModel';
import { IUserModel } from '@data/user/userModel.interface';
import { IRequestResultModel } from '@data/requestResultModel.interface';

const apiUrl = `${environment.apiUrl}/user`;

@Injectable()
export class UserService {
  private userObjStorageKey: string = 'userData';

  constructor(
    private storageService: StorageService,
    private http: HttpClient
  ) {}

  public loginEvent: EventEmitter<any> = new EventEmitter();
  public logoutEvent: EventEmitter<any> = new EventEmitter();

  usernameExists(username: string): Observable<IRequestResultModel<boolean>> {
    return this.http
      .get(`${apiUrl}/usernameExists?username=${username}`)
      .pipe(map(extractResponse<boolean>));
  }

  register(
    userData: UserRegisterModel
  ): Observable<IRequestResultModel<IUserModel>> {
    return this.http.post(`${apiUrl}/register`, userData).pipe(
      map(extractResponse<IUserModel>),
      tap((response: IRequestResultModel<IUserModel>) => {
        if (response.isSuccessful) {
          this.storageService.setItem(this.userObjStorageKey, response.data);

          this.loginEvent.emit();
        }
      })
    );
  }

  login(userData: UserLoginModel): Observable<IRequestResultModel<IUserModel>> {
    return this.http.post(`${apiUrl}/login`, userData).pipe(
      map(extractResponse<IUserModel>),
      tap((response: IRequestResultModel<IUserModel>) => {
        if (response.isSuccessful) {
          this.storageService.setItem(this.userObjStorageKey, response.data);

          this.loginEvent.emit();
        }
      })
    );
  }

  logout() {
    this.storageService.setItem(this.userObjStorageKey, null);
    this.logoutEvent.emit();
  }

  isOfRole(role: string): boolean {
    if (this.isLoggedIn) {
      return this.getUserObj().roles.includes(role);
    }

    return false;
  }

  getJwtToken(): string {
    if (this.isLoggedIn) {
      return this.getUserObj().jwt;
    }

    return '';
  }

  getUsername(): string {
    if (this.isLoggedIn) {
      return this.getUserObj().username;
    }

    return '';
  }

  get isLoggedIn(): boolean {
    const user = this.getUserObj();
    return user !== null && typeof user === 'object';
  }

  private getUserObj(): IUserModel {
    return this.storageService.getItem(this.userObjStorageKey);
  }
}
