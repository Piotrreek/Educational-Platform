import classes from "./Actions.module.css";

const Actions = ({ children, className }) => {
  return (
    <div
      className={`${classes.actions} ${!!className ? className : undefined}`}
    >
      {children}
    </div>
  );
};

export default Actions;
