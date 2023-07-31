import Logo from "./Logo";
import classes from "./Header.module.css";
import Nav from "./Nav";

const Header = () => {
  return (
    <div className={classes['header-container']}>
         <header className={classes.header}>
      <Logo />
      <Nav />
    </header>
    </div>
   
  );
};

export default Header;
