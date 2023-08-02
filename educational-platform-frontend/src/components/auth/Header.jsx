import classes from "./Header.module.css";

const Header = ({ heading }) => {
  return <h1 className={classes.header}>{heading}</h1>;
};

export default Header;
