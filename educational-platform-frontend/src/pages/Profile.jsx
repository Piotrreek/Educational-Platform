import ChooseAcademyEntitiesForm from "../components/auth/profile/ChooseAcademyEntitiesForm";
import ProfileActions from "../components/auth/profile/ProfileActions";
import Container from "../components/ui/Container";

import classes from "../components/auth/profile/ChooseAcademyEntitiesForm.module.css";
import Header from "../components/auth/Header";

const Profile = () => {
  return (
    <Container className={classes["flex-start"]}>
      <div className={classes.container}>
        <Header heading="Przypisz się do uczelni, wydziału i kierunku" />
        <ChooseAcademyEntitiesForm />
      </div>
      <div className={classes.container}>
        <Header heading="Dostępne akcje" />
        <ProfileActions />
      </div>
    </Container>
  );
};

export default Profile;
