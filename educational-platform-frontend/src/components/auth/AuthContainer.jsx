import Container from "../ui/Container";
import classes from "./AuthContainer.module.css";

const AuthContainer = ({ children, innerContainerClassName, className }) => {
  return (
    <Container className={className}>
      <div
        className={`${classes.container} ${
          !!innerContainerClassName ? innerContainerClassName : undefined
        }`}
      >
        {children}
      </div>
    </Container>
  );
};

export default AuthContainer;
