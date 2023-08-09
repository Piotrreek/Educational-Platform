import classes from "./Backdrop.module.css";

const Backdrop = ({ onClick, children }) => {
  return (
    <>
      <div className={classes.backdrop} onClick={onClick} />
      {children}
    </>
  );
};

export default Backdrop;
