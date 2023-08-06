import classes from "./LogoutForm.module.css";
import { Form } from "react-router-dom";

const LogoutForm = () => {
  return (
    <Form action="/logout" method="POST">
      <button className={classes.logoutBtn}>Wyloguj</button>
    </Form>
  );
};

export default LogoutForm;
