import useInput from "../../../hooks/useInput";
import { useState } from "react";

import Input from "../../ui/Input";
import Actions from "../Actions";
import Button from "../../ui/Button";

import {
  validatePassword,
  validateConfirmPassword,
  notEmpty,
} from "../../../utils/validators";
import { getToken } from "../../../utils/jwtUtils";

import classes from "../../ui/Form.module.css";

const ChangePasswordForm = ({ onClose }) => {
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState();
  const [isSuccess, setIsSuccess] = useState(false);

  const {
    value: oldPassword,
    isValid: oldPasswordIsValid,
    hasError: oldPasswordHasError,
    error: oldPasswordError,
    onChange: onOldPasswordChange,
    onBlur: onOldPasswordBlur,
  } = useInput(notEmpty);

  const {
    value: newPassword,
    isValid: newPasswordIsValid,
    hasError: newPasswordHasError,
    error: newPasswordError,
    onChange: onNewPasswordChange,
    onBlur: onNewPasswordBlur,
  } = useInput(validatePassword);

  const {
    value: confirmNewPassword,
    isValid: confirmNewPasswordIsValid,
    hasError: confirmNewPasswordHasError,
    error: confirmNewPasswordError,
    onChange: onConfirmNewPasswordChange,
    onBlur: onConfirmNewPasswordBlur,
  } = useInput(validateConfirmPassword.bind(null, newPassword));

  const onSubmit = async (e) => {
    e.preventDefault();
    try {
      setSubmitting(true);
      const response = await fetch(
        `${process.env.REACT_APP_BACKEND_URL}user/change-password`,
        {
          method: "PATCH",
          credentials: "include",
          body: JSON.stringify({
            oldPassword: oldPassword,
            newPassword: newPassword,
            confirmNewPassword: confirmNewPassword,
          }),
          headers: {
            Authorization: `Bearer ${getToken()}`,
            "Content-Type": "application/json",
          },
        }
      );

      if (response.status === 400) {
        const data = (await response.json()).message;

        if (!!data) {
          setError(data);

          setTimeout(() => {
            setError(null);
          }, 2000);
        }
      }

      if (response.ok) {
        setIsSuccess(true);
        setTimeout(() => {
          onClose();
        }, 4000);
      }
    } catch (_) {}
    setSubmitting(false);
  };

  const formStateIsValid =
    oldPasswordIsValid && newPasswordIsValid && confirmNewPasswordIsValid;

  return (
    <form className={classes.form} onSubmit={onSubmit}>
      {isSuccess && (
        <p className={classes.success}>Pomyślnie zmieniono hasło</p>
      )}
      {!!error && <p className={classes.error}>{error}</p>}
      <Input
        id="oldPassword"
        name="oldPassword"
        label="Stare hasło"
        type="password"
        value={oldPassword}
        hasError={oldPasswordHasError}
        error={oldPasswordError}
        onChange={onOldPasswordChange}
        onBlur={onOldPasswordBlur}
      />
      <Input
        id="newPassword"
        name="newPassword"
        label="Nowe hasło"
        type="password"
        value={newPassword}
        hasError={newPasswordHasError}
        error={newPasswordError}
        onChange={onNewPasswordChange}
        onBlur={onNewPasswordBlur}
      />
      <Input
        id="confirmNewPassword"
        name="confirmNewPassword"
        label="Potwierdź nowe hasło"
        type="password"
        value={confirmNewPassword}
        hasError={confirmNewPasswordHasError}
        error={confirmNewPasswordError}
        onChange={onConfirmNewPasswordChange}
        onBlur={onConfirmNewPasswordBlur}
      />
      <Actions className={classes["actions-right"]}>
        <Button disabled={!formStateIsValid}>
          {submitting ? "Zmieniam..." : "Zmień hasło"}
        </Button>
      </Actions>
    </form>
  );
};

export default ChangePasswordForm;
