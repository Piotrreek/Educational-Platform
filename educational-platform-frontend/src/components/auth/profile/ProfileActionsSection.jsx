import { useState } from "react";

import { Link } from "react-router-dom";
import Modal from "../../ui/Modal";
import AuthContainer from "../AuthContainer";
import Header from "../Header";
import ChangePasswordForm from "./ChangePasswordForm";

import classes from "./ProfileContent.module.css";
import formClasses from "../../ui/Form.module.css";

const ProfileActionsSection = ({ emailConfirmed }) => {
  const [changePasswordModalOpened, setChangePasswordModalOpened] =
    useState(false);

  return (
    <section className={classes["profile-actions-section"]}>
      <h2>Dostępne akcje</h2>
      <ul>
        <li>
          <button
            onClick={() => setChangePasswordModalOpened(true)}
            className={classes.linkBtn}
          >
            Zmień hasło
          </button>
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

      {changePasswordModalOpened && (
        <Modal
          onClose={() => setChangePasswordModalOpened(false)}
          className={classes.actions__modal}
          showCloseWord={true}
        >
          <AuthContainer
            innerContainerClassName={`${formClasses["scroll-auto"]} ${formClasses["mt-80"]}`}
          >
            <Header heading="Zmień hasło" />
            <ChangePasswordForm
              onClose={() => setChangePasswordModalOpened(false)}
            />
          </AuthContainer>
        </Modal>
      )}
    </section>
  );
};

export default ProfileActionsSection;
