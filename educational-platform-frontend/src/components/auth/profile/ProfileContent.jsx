import ProfileDataSection from "./ProfileDataSection";
import ChooseAcademyEntitiesFormSection from "./ChooseAcademyEntitiesFormSection";
import ProfileActionsSection from "./ProfileActionsSection";

import photo from "../../../assets/profile-photo.svg";

import classes from "./ProfileContent.module.css";

const ProfileContent = ({ user, setUser }) => {
  return (
    <div className={classes.profile}>
      <div className={classes["profile-header"]}>
        <h2 className={classes["profile-header__photo-header"]}>
          Zdjęcie profilowe
        </h2>
        <div className={classes["profile-header__content"]}>
          <div className={classes["profile-header__photo"]}>
            <img src={photo} alt="profile" />
          </div>
          <div className={classes.actions}>
            <a href="#">Usuń</a>
            <a href="#">Wgraj</a>
          </div>
        </div>
      </div>
      <ProfileDataSection user={user} />
      <ChooseAcademyEntitiesFormSection user={user} setUser={setUser} />
      <ProfileActionsSection emailConfirmed={user.emailConfirmed} />
    </div>
  );
};

export default ProfileContent;
