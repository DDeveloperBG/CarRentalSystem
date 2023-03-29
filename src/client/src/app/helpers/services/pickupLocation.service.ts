import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';

import { extractResponse } from '@utils/response.utils';
import { IRequestResultModel } from '@data/requestResultModel.interface';

import { IOneOfGetAllPickupLocations } from '@data/pickupLocation/oneOfGetAllPickupLocations.interface';

import { map, Observable } from 'rxjs';

const apiUrl = `${environment.apiUrl}/pickupLocation`;

@Injectable()
export class PickupLocationService {
  constructor(private http: HttpClient) {}

  getAll(): Observable<IRequestResultModel<IOneOfGetAllPickupLocations[]>> {
    return this.http
      .get(`${apiUrl}/getAll`)
      .pipe(map(extractResponse<IOneOfGetAllPickupLocations[]>));
  }
}
