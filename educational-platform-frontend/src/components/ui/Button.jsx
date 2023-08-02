import classes from "./Button.module.css";

const Button = ({ children, onClick, disabled }) => {
  return (
    <button className={classes.button} onClick={onClick} disabled={disabled}>
      {children}
    </button>
  );
};

export default Button;
