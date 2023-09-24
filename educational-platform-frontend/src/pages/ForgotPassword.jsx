import AuthContainer from "../components/auth/AuthContainer";
import ForgotPasswordForm from "../components/auth/ForgotPasswordForm";
import Header from "../components/auth/Header";

const ForgotPassword = () => {
  return (
    <AuthContainer>
      <Header heading="Nie pamiętam hasła" />
      <ForgotPasswordForm />
    </AuthContainer>
  );
};

export default ForgotPassword;
