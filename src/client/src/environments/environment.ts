export const environment = {
  production: false,
  apiUrl: 'https://localhost:44349/api',
  validationConstants: {
    user: {
      username: {
        minLength: 3,
        maxLength: 30,
      },
      password: {
        requireDigit: true,
        requireDigitRegex: /\d/u,
        requireLowercase: true,
        requireLowercaseRegex: /\p{Ll}/u,
        requireUppercase: true,
        requireUppercaseRegex: /\p{Lu}/u,
        requireNonAlphanumeric: false,
        requireNonAlphanumericRegex: /\W/u,
        minLength: 6,
        maxLength: 30,
      },
      forename: {
        minLength: 2,
        maxLength: 40,
      },
      surname: { minLength: 2, maxLength: 40 },
      pin: { requiredLength: 10 },
      phoneNumber: { regex: /\d+/u },
    },
    common: {
      isAllDigitsRegex: /^\d+$/u,
      isAllLettersRegex: /^\p{L}+$/u,
    },
  },
};
