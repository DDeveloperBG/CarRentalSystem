export const globalValues = {
  car: {
    defaultPassengerSeats: 4,
    defaultTransmissionType: 0,
    transmissionTypes: ['Manual', 'Automatic'],
  },
  roles: {
    admin: 'admin',
    user: 'user',
  },
  errorMessages: {
    user: {
      forenameLength: 'Forename must be between 2 and 40 characters!',
      forenameInvalidChars: 'Forename must contain only alphabet characters!',
      surnameLength: 'Surname must be between 2 and 40 characters!',
      surnameInvalidChars: 'Surname must contain only alphabet characters!',
      pinInvalidChars: 'PIN should contain only digits!',
      pinLength: 'PIN length should be 10!',
      phoneNumberInvalid: 'Phone number is invalid!',
      usernameLength: 'Username must be between 3 and 30 characters!',
      usernameExists: 'Username already exists!',
      emailInvalid: 'Email is invalid!',
      passwordLength: 'Password must be between 6 and 30 characters!',
      passwordRequireDigit: 'Password must contain at least one digit!',
      passwordRequireLowercase:
        'Password must contain at least one lowercase character!',
      passwordRequireUppercase:
        'Password must contain at least one upper case character!',
      passwordRequireNonAlphanumeric:
        'Password must contain at least one non-alphanumeric character!',
      confirmPasswordRequireMatch: 'Password do not match!',
    },
  },
};
