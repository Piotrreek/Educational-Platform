export const notEmpty = (value) => {
  if (!value) {
    return { isValid: false, error: "To pole nie może być puste" };
  }

  return { isValid: true };
};

export const isPdf = (value) => {
  if (!value) {
    return { isValid: false, error: "To pole nie może być puste" };
  }

  if (!(value.type === "application/pdf")) {
    return { isValid: false, error: "Przekaż plik PDF" };
  }

  return { isValid: true };
};

export const validateEmail = (email) => {
  if (!email) {
    return { isValid: false, error: "To pole nie może być puste" };
  }

  if (
    !email.match(
      /^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w.-]*[a-z0-9]\.[a-z][a-z.]*[a-z]$/
    )
  ) {
    return {
      isValid: false,
      error: "Podany adres e-mail jest niepoprawny",
    };
  }

  return { isValid: true };
};

export const validateLoginPassword = (password) => {
  if (!password) {
    return { isValid: false, error: "To pole nie może być puste" };
  }

  return { isValid: true };
};

export const validatePassword = (password) => {
  if (!password) {
    return { isValid: false, error: "To pole nie może być puste" };
  }

  if (
    !password.match(
      /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/
    )
  ) {
    return {
      isValid: false,
      error:
        "Podane hasło musi się składać z conajmniej jednej małej i wielkiej litery, jednej cyfry i jednego znaku specjalnego, a także musi mieć co najmniej 8 znaków",
    };
  }

  return { isValid: true };
};

export const validateConfirmPassword = (password, confirmPassword) => {
  if (!confirmPassword) {
    return { isValid: false, error: "To pole nie może być puste" };
  }

  if (!(confirmPassword === password)) {
    return {
      isValid: false,
      error: "Hasło oraz potwierdzenie hasła muszą być identyczne",
    };
  }

  return { isValid: true };
};
