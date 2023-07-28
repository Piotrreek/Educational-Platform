import { Link } from "react-router-dom";
import LogoSvg from "../../assets/logo.svg";

import classes from "./Logo.module.css";

const Logo = () => {
  return (
    <div className={classes.logo}>
      <Link to="/">
        <img src={LogoSvg} alt="Logo" />
        <span>Baza</span>
      </Link>
    </div>
  );
};

export default Logo;
