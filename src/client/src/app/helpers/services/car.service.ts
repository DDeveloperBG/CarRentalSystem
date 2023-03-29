import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';

import { extractResponse, extractResponseNoData } from '@utils/response.utils';
import {
  IRequestResultModel,
  IRequestResultModelNoData,
} from '@data/requestResultModel.interface';
import { map, Observable } from 'rxjs';

import { IAllCarAds } from '@data/car/allCarAds.interface';
import { ICarAdDetails } from '@data/car/carAdDetails.interface';
import { AllCarAdsInRangeInput } from '@data/car/allCarAdsInRangeInput';
import { AddCarAdRentRequest } from '@data/car/addCarAdRentRequest';
import { IOneOfAllUserRentingRequests } from '@data/car/oneOfAllUserRentingRequests.interface';
import { IOneOfAllUnconfirmedRentRequests } from '@data/car/oneOfAllUnconfirmedRentRequests.interface';

const apiUrl = `${environment.apiUrl}/car`;

@Injectable()
export class CarService {
  constructor(private http: HttpClient) {}

  confirmRentingRequest(
    requestId: string
  ): Observable<IRequestResultModelNoData> {
    const body = new FormData();
    body.set('requestId', requestId);

    return this.http
      .post(`${apiUrl}/confirmRentingRequest`, body)
      .pipe(map(extractResponseNoData));
  }

  getUnconfirmedRentRequestsAsync(): Observable<
    IRequestResultModel<IOneOfAllUnconfirmedRentRequests[]>
  > {
    return this.http
      .get(`${apiUrl}/getAllUnconfirmedRentingRequests`)
      .pipe(map(extractResponse<IOneOfAllUnconfirmedRentRequests[]>));
  }

  getAllUserRentingRequest(
    forThePast: boolean
  ): Observable<IRequestResultModel<IOneOfAllUserRentingRequests[]>> {
    return this.http
      .get(`${apiUrl}/getAllUserRentingRequests?forThePast=${forThePast}`)
      .pipe(map(extractResponse<IOneOfAllUserRentingRequests[]>));
  }

  addRentingRequest(
    data: AddCarAdRentRequest
  ): Observable<IRequestResultModelNoData> {
    return this.http
      .post(`${apiUrl}/addRentingRequest`, data)
      .pipe(map(extractResponseNoData));
  }

  deleteAdvertisement(
    advertisementId: string
  ): Observable<IRequestResultModelNoData> {
    const body = new FormData();
    body.set('advertisementId', advertisementId);

    return this.http
      .post(`${apiUrl}/deleteAdvertisement`, body)
      .pipe(map(extractResponseNoData));
  }

  getOneAdDetails(
    adId: string
  ): Observable<IRequestResultModel<ICarAdDetails>> {
    return this.http
      .get(`${apiUrl}/getAdvertisementDetails?advertisementId=${adId}`)
      .pipe(map(extractResponse<ICarAdDetails>));
  }

  getAllAds(
    period: AllCarAdsInRangeInput | undefined
  ): Observable<IRequestResultModel<IAllCarAds>> {
    let queryParameters: string = '';
    if (period != undefined) {
      queryParameters = `?FromDate=${period.fromDate}&ToDate=${period.toDate}`;
    }

    return this.http
      .get(`${apiUrl}/getAllAdvertisements${queryParameters}`)
      .pipe(map(extractResponse<IAllCarAds>));
  }

  addAdvertisement(carData: any): Observable<IRequestResultModelNoData> {
    return this.http
      .post(`${apiUrl}/addAdvertisement`, carData)
      .pipe(map(extractResponseNoData));
  }
}
