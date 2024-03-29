import { useState } from "react";

import { Link } from "react-router-dom";
import Modal from "../../ui/Modal";
import AuthContainer from "../AuthContainer";
import Header from "../Header";
import ChangePasswordForm from "./ChangePasswordForm";
import CreateAcademyEntityRequestForm from "../../forms/CreateAcademyEntityRequestForm";

import { AcademyEntityTypes } from "../../../utils/academyEntityTypes";

import classes from "./ProfileContent.module.css";
import formClasses from "../../ui/Form.module.css";

const ProfileActionsSection = ({ user }) => {
  const [changePasswordModalOpened, setChangePasswordModalOpened] =
    useState(false);
  const [academyModalType, setAcademyModalType] = useState("");

  const getAcademyModalTypeHeading = () => {
    switch (academyModalType) {
      case AcademyEntityTypes.University:
        return "Wyślij prośbę o dodanie uczelni";
      case AcademyEntityTypes.Faculty:
        return "Wyślij prośbę o dodanie wydziału";
      case AcademyEntityTypes.UniversitySubject:
        return "Wyślij prośbę o dodanie kierunku";
      case AcademyEntityTypes.UniversityCourse:
        return "Wyślij prośbę o dodanie przedmiotu";
      default:
        return;
    }
  };

  return (
    <section
      className={`${classes["profile-actions-section"]} content__section`}
    >
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
        {!user.emailConfirmed && (
          <li>
            <Link to="reset-password">
              Wyślij link do potwierdzenia konta na email
            </Link>
          </li>
        )}
        {!!user.universityId && (
          <li>
            <Link to={`/didactic-material?universityId=${user.universityId}`}>
              Zobacz materiału dla swojej uczelni
            </Link>
          </li>
        )}
        {!!user.facultyId && (
          <li>
            <Link
              to={`/didactic-material?universityId=${user.universityId}&facultyId=${user.facultyId}`}
            >
              Zobacz materiału dla swojego wydziału
            </Link>
          </li>
        )}
        {!!user.subjectId && (
          <li>
            <Link
              to={`/didactic-material?universityId=${user.universityId}&facultyId=${user.facultyId}&subjectId=${user.subjectId}`}
            >
              Zobacz materiału dla swojego kierunku
            </Link>
          </li>
        )}
        <li>
          <button
            onClick={() => setAcademyModalType(AcademyEntityTypes.University)}
            className={classes.linkBtn}
          >
            Wyślij prośbę o dodanie uczelni
          </button>
        </li>
        <li>
          <button
            onClick={() => setAcademyModalType(AcademyEntityTypes.Faculty)}
            className={classes.linkBtn}
          >
            Wyślij prośbę o dodanie wydziału
          </button>
        </li>
        <li>
          <button
            onClick={() =>
              setAcademyModalType(AcademyEntityTypes.UniversitySubject)
            }
            className={classes.linkBtn}
          >
            Wyślij prośbę o dodanie kierunku
          </button>
        </li>
        <li>
          <button
            onClick={() =>
              setAcademyModalType(AcademyEntityTypes.UniversityCourse)
            }
            className={classes.linkBtn}
          >
            Wyślij prośbę o dodanie przedmiotu
          </button>
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
      {!!academyModalType && (
        <Modal
          onClose={() => setAcademyModalType("")}
          className={classes.actions__modal}
          showCloseWord={true}
        >
          <AuthContainer
            innerContainerClassName={`${formClasses["scroll-auto"]} ${formClasses["mt-80"]}`}
          >
            <Header heading={getAcademyModalTypeHeading()} />
            <CreateAcademyEntityRequestForm
              type={academyModalType}
              onClose={() => setAcademyModalType("")}
            />
          </AuthContainer>
        </Modal>
      )}
    </section>
  );
};

export default ProfileActionsSection;
