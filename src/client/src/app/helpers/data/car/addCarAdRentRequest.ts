export class AddCarAdRentRequest {
  constructor(
    public fromDate: string,
    public toDate: string,
    public pickupLocationId: string,
    public carId: string
  ) {}
}
