import { Link } from "react-router-dom";

import classes from "./ProfileContent.module.css";

const ProfileActionsSection = ({ emailConfirmed }) => {
  return (
    <section className={classes["profile-actions-section"]}>
      <h2>Dostępne akcje</h2>
      <ul>
        <li>
          <Link to="reset-password">Zmień hasło</Link>
        </li>
        {!emailConfirmed && (
          <li>
            <Link to="reset-password">
              Wyślij link do potwierdzenia konta na email
            </Link>
          </li>
        )}
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
    </section>
  );
};

export default ProfileActionsSection;
