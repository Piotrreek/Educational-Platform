import { Container } from "../ui/Container";
import classes from "./AuthContainer.module.css";

const AuthContainer = ({ children }) => {
  return (
    <Container>
      <div className={classes.container}>{children}</div>
    </Container>
  );
};

export default AuthContainer;
