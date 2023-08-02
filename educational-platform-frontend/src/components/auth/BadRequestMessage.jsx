import classes from "./BadRequestMessage.module.css";

const BadRequestMessage = ({ message }) => {
  return <p className={classes.error}>{message}</p>;
};

export default BadRequestMessage;
