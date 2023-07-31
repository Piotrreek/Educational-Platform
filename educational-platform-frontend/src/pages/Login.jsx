import LoginForm from "../components/auth/LoginForm";
import Container from "../components/ui/Container";
import classes from "./Login.module.css";

const Login = () => {
  return (
    <Container className={classes['max-height']}>
      <div className={classes['login-container']}>
        <h1 className={classes.header}>Logowanie</h1>
        <LoginForm />
      </div>
    </Container>
  );
};

export default Login;
