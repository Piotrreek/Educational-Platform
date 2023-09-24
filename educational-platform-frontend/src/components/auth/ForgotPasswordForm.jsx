import { useState } from "react";
import useInput from "../../hooks/useInput";

import Input from "../ui/Input";
import Actions from "./Actions";
import Button from "../ui/Button";

import { validateEmail } from "../../utils/validators";
import { forgotPassword } from "../../api/authApi";

import classes from "../ui/Form.module.css";

const ForgotPasswordForm = () => {
  const [error, setError] = useState();
  const [isSuccess, setIsSuccess] = useState(false);

  const {
    value: email,
    isValid: emailIsValid,
    hasError: emailHasError,
    error: emailError,
    onChange: onEmailChange,
    onBlur: onEmailBlur,
  } = useInput(validateEmail);

  const onSubmit = async (e) => {
    e.preventDefault();

    const response = await forgotPassword(email);

    if (!!response.error) {
      setError(response.error);

      setTimeout(() => {
        setError();
      }, 2000);
      return;
    }

    setIsSuccess(true);

    setTimeout(() => {
      setIsSuccess(false);
    }, 2000);
  };

  return (
    <form className={classes.form} onSubmit={onSubmit}>
      {isSuccess && (
        <p className={classes.success}>
          Pomyślnie wysłano link do zmiany hasła na podany adres e-mail
        </p>
      )}
      {!!error && <p className={classes.error}>{error}</p>}
      <Input
        id="email"
        name="email"
        label="E-mail"
        type="text"
        value={email}
        hasError={emailHasError}
        error={emailError}
        onChange={onEmailChange}
        onBlur={onEmailBlur}
      />
      <Actions className={classes["actions-right"]}>
        <Button disabled={!emailIsValid}>Wyślij</Button>
      </Actions>
    </form>
  );
};

export default ForgotPasswordForm;
