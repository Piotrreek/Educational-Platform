import OuterContainer from "../../ui/Container";
import classes from "./ContentContainer.module.css";

const ContentContainer = ({ children }) => {
  return (
    <OuterContainer>
      <div className={classes.container}>{children}</div>
    </OuterContainer>
  );
};

export default ContentContainer;
