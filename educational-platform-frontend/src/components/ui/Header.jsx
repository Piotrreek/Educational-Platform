import Logo from "./Logo";
import classes from "./Header.module.css";
import Nav from "./Nav";
import Container from "./Container";

const Header = () => {
  return (
    <Container>
         <header className={classes.header}>
      <Logo />
      <Nav />
    </header>
    </Container>
   
  );
};

export default Header;
