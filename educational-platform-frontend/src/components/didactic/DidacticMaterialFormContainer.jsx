import { Container } from "../ui/Container";
import classes from "./DidacticMaterialFormContainer.module.css";

const DidacticMaterialFormContainer = ({ children }) => {
  return (
    <Container>
      <div className={classes.container}>{children}</div>
    </Container>
  );
};

export default DidacticMaterialFormContainer;
