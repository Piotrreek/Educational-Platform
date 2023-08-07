import { Container as OuterContainer } from "../../ui/Container";
import classes from "./Container.module.css";

const Container = ({ children }) => {
  return (
    <OuterContainer>
      <div className={classes.container}>{children}</div>
    </OuterContainer>
  );
};

export default Container;
