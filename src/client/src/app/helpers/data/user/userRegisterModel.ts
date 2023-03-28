export class UserRegisterModel {
  constructor(
    public forename: string,
    public surname: string,
    public personalIdentificationNumber: string,
    public phoneNumber: string,
    public email: string,
    public username: string,
    public password: string
  ) {}
}
