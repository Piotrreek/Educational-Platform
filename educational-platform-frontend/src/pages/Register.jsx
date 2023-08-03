import RegisterForm from "../components/auth/RegisterForm";
import Header from "../components/auth/Header";
import AuthContainer from "../components/auth/AuthContainer";

const Register = () => {
  return (
    <AuthContainer>
      <Header heading="Rejestracja" />
      <RegisterForm />
    </AuthContainer>
  );
};

export default Register;
