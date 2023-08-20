import classes from "./FormContainer.module.css";

const FormContainer = ({ children }) => {
  return <div className={classes.container}>{children}</div>;
};

export default FormContainer;
