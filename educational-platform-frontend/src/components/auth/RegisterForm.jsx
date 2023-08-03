import {
  Form,
  Link,
  useActionData,
  useNavigate,
  useNavigation,
} from "react-router-dom";
import Input from "../ui/Input";
import Actions from "./Actions";
import classes from "./RegisterForm.module.css";
import Button from "../ui/Button";
import Helpers from "./Helpers";
import BadRequestMessage from "./BadRequestMessage";
import useInput from "../../hooks/useInput";
import {
  notEmpty,
  validateConfirmPassword,
  validateEmail,
  validatePassword,
} from "../../utils/validators";

const RegisterForm = () => {
  const navigation = useNavigation();
  const navigate = useNavigate();
  const actionData = useActionData();
  const actionDataMessage = actionData?.message;

  const {
    value: email,
    isValid: emailIsValid,
    hasError: emailHasError,
    error: emailError,
    onChange: onEmailChange,
    onBlur: onEmailBlur,
  } = useInput(validateEmail);

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

  const {
    value: username,
    isValid: usernameIsValid,
    hasError: usernameHasError,
    error: usernameError,
    onChange: onUsernameChange,
    onBlur: onUsernameBlur,
  } = useInput(notEmpty);

  const formIsValid =
    emailIsValid &&
    usernameIsValid &&
    passwordIsValid &&
    confirmPasswordIsValid;
  const isSubmitting = navigation.state === "submitting";

  if (!!actionData?.isSuccess) {
    navigate("/login", { state: { registered: true } });
  }

  return (
    <Form className={classes["register-form"]} method="POST">
      {actionDataMessage && <BadRequestMessage message={actionDataMessage} />}
      <Input
        id="username"
        name="username"
        label="Nazwa użytkownika"
        type="text"
        value={username}
        hasError={usernameHasError}
        error={usernameError}
        onChange={onUsernameChange}
        onBlur={onUsernameBlur}
      />
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
      <Input id="role" name="role" type="hidden" value="User" />
      <Actions>
        <Helpers>
          <Link to="/login">Posiadam już konto (zaloguj)</Link>
        </Helpers>
        <div>
          <Button disabled={!formIsValid || isSubmitting}>
            {isSubmitting ? "Rejestruję..." : "Zarejestruj"}
          </Button>
        </div>
      </Actions>
    </Form>
  );
};

export default RegisterForm;
