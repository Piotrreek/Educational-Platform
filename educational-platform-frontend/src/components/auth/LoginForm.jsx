import {
  Form,
  Link,
  useActionData,
  useLocation,
  useNavigation,
} from "react-router-dom";
import Button from "../ui/Button";
import classes from "./LoginForm.module.css";
import Input from "../ui/Input";
import Actions from "./Actions";
import Helpers from "./Helpers";
import useInput from "../../hooks/useInput";
import { validateEmail, validateLoginPassword } from "../../utils/validators";
import BadRequestMessage from "./BadRequestMessage";

const LoginForm = () => {
  const navigation = useNavigation();
  const actionData = useActionData();
  const location = useLocation();
  const actionDataMessage = actionData?.message;
  const registered = location.state?.registered;

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
  } = useInput(validateLoginPassword);

  const formIsValid = emailIsValid && passwordIsValid;
  const isSubmitting = navigation.state === "submitting";
  const registeredMessage = (
    <p className={classes.registered}>
      Zarejestrowano pomyślnie. Teraz możesz się zalogować
    </p>
  );

  return (
    <>
      <Form className={classes["login-form"]} method="POST">
        {actionDataMessage && <BadRequestMessage message={actionDataMessage} />}
        {registered && registeredMessage}
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
        <Actions>
          <Helpers>
            <Link to="/register">Zarejestruj się</Link>
            <Link to="/forgot-password">Nie pamiętam hasła</Link>
          </Helpers>
          <div>
            <Button disabled={!formIsValid || isSubmitting}>
              {isSubmitting ? "Loguję..." : "Zaloguj"}
            </Button>
          </div>
        </Actions>
      </Form>
    </>
  );
};

export default LoginForm;
