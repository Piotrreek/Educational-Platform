import classes from "./Header.module.css";

const Header = ({ heading, className }) => {
  return (
    <h1 className={`${classes.header} ${className && className}`}>{heading}</h1>
  );
};

export default Header;
