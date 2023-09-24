import { useState } from "react";
import useInput from "../../hooks/useInput";

import Input from "../ui/Input";
import Actions from "./Actions";
import Button from "../ui/Button";

import {
  validatePassword,
  validateConfirmPassword,
} from "../../utils/validators";

import classes from "../ui/Form.module.css";
import { useNavigate, useParams, useSearchParams } from "react-router-dom";
import { resetPassword } from "../../api/authApi";

const ResetPasswordForm = () => {
  const [error, setError] = useState();
  const [isSuccess, setIsSuccess] = useState(false);
  const params = useParams();
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();

  const {
    value: password,
    isValid: passwordIsValid,
    hasError: passwordHasError,
    error: passwordError,
    onChange: onPasswordChange,
    onBlur: onPasswordBlur,
  } = useInput(validatePassword);

  const {
    value: confirmPassword,
    isValid: confirmPasswordIsValid,
    hasError: confirmPasswordHasError,
    error: confirmPasswordError,
    onChange: onConfirmPasswordChange,
    onBlur: onConfirmPasswordBlur,
  } = useInput(validateConfirmPassword.bind(null, password));

  const onSubmit = async (e) => {
    e.preventDefault();

    const response = await resetPassword(
      password,
      confirmPassword,
      params.userId,
      searchParams.get("token")
    );

    if (!!response?.error) {
      setError(response.error);

      setTimeout(() => {
        setError();
      }, 2000);
      return;
    }

    setIsSuccess(true);

    setTimeout(() => {
      navigate("/login");
    }, 2000);
  };

  return (
    <form className={classes.form} onSubmit={onSubmit}>
      {isSuccess && (
        <p className={classes.success}>Pomyślnie zmieniono hasło</p>
      )}
      {!!error && <p className={classes.error}>{error}</p>}
      <Input
        id="password"
        name="password"
        label="Hasło"
        type="password"
        value={password}
        hasError={passwordHasError}
        error={passwordError}
        onChange={onPasswordChange}
        onBlur={onPasswordBlur}
      />
      <Input
        id="confirm-password"
        name="confirm-password"
        label="Potwierdź hasło"
        type="password"
        value={confirmPassword}
        hasError={confirmPasswordHasError}
        error={confirmPasswordError}
        onChange={onConfirmPasswordChange}
        onBlur={onConfirmPasswordBlur}
      />
      <Actions className={classes["actions-right"]}>
        <Button disabled={!passwordIsValid || !confirmPasswordIsValid}>
          Zresetuj hasło
        </Button>
      </Actions>
    </form>
  );
};

export default ResetPasswordForm;
