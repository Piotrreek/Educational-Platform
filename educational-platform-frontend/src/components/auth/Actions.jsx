import classes from "./Actions.module.css";

const Actions = ({ children }) => {
  return <div className={classes.actions}>{children}</div>;
};

export default Actions;