import AuthContainer from "../components/auth/AuthContainer";
import Header from "../components/auth/Header";
import ResetPasswordForm from "../components/auth/ResetPasswordForm";

const ResetPassword = () => {
  return (
    <AuthContainer>
      <Header heading="Utwórz nowe hasło" />
      <ResetPasswordForm />
    </AuthContainer>
  );
};

export default ResetPassword;
