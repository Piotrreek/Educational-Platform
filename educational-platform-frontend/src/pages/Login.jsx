import AuthContainer from "../components/auth/AuthContainer";
import Header from "../components/auth/Header";
import LoginForm from "../components/auth/LoginForm";

const Login = () => {
  return (
    <AuthContainer>
      <Header heading="Logowanie" />
      <LoginForm />
    </AuthContainer>
  );
};

export default Login;
