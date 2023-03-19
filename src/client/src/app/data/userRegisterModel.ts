export class UserRegisterModel {
  forename: string;
  surname: string;
  personalIdentificationNumber: string;
  phoneNumber: string;
  email: string;
  username: string;
  password: string;

  constructor(
    forename: string,
    surname: string,
    personalIdentificationNumber: string,
    phoneNumber: string,
    email: string,
    username: string,
    password: string
  ) {
    this.forename = forename;
    this.surname = surname;
    this.personalIdentificationNumber = personalIdentificationNumber;
    this.phoneNumber = phoneNumber;
    this.email = email;
    this.username = username;
    this.password = password;
  }
}
