import { Link } from "react-router-dom";

import classes from "./ProfileActions.module.css";

const ProfileActions = () => {
  return (
    <ul className={classes.actions}>
      <li>
        <Link to="reset-password">Zresetuj hasło</Link>
      </li>
      <li>
        <Link to="reset-password">
          Wyślij link do potwierdzenia konta na email
        </Link>
      </li>
      <li>
        <Link to="reset-password">Wyślij prośbę o dodanie uczelni</Link>
      </li>
      <li>
        <Link to="reset-password">Wyślij prośbę o dodanie wydziału</Link>
      </li>
      <li>
        <Link to="reset-password">Wyślij prośbę o dodanie kierunku</Link>
      </li>
      <li>
        <Link to="reset-password">Wyślij prośbę o dodanie przedmiotu</Link>
      </li>
    </ul>
  );
};

export default ProfileActions;
