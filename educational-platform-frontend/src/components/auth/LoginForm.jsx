import { Link } from "react-router-dom";
import Button from "../ui/Button";
import classes from "./LoginForm.module.css";

const LoginForm = () => {
  return (
    <form className={classes["login-form"]}>
      <div className="input-container">
        <label htmlFor="email">E-mail</label>
        <input type="text" id="email" name="email" />
        <span>123123123</span>
      </div>
      <div className="input-container">
        <label htmlFor="password">Hasło</label>
        <input type="password" id="password" name="password" />
        <span></span>
      </div>
      <div className={classes.actions}>
        <div className={classes.helpers}>
          <Link to="/register">Zarejestruj się</Link>
          <Link to="/forgot-password">Nie pamiętam hasła</Link>
        </div>
        <div>
          <Button>Zaloguj</Button>
        </div>
      </div>
    </form>
  );
};

export default LoginForm;
