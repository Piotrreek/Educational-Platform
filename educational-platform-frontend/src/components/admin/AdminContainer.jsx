import Container from "../ui/Container";

import classes from "./AdminContainer.module.css";

const AdminContainer = ({ children }) => {
  return (
    <Container>
      <div className={classes.container}>{children}</div>
    </Container>
  );
};

export default AdminContainer;
