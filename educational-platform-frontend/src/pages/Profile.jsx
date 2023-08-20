import { useLoaderData } from "react-router-dom";
import { useState } from "react";

import ChooseAcademyEntitiesForm from "../components/auth/profile/ChooseAcademyEntitiesForm";
import ProfileActions from "../components/auth/profile/ProfileActions";
import Container from "../components/ui/Container";
import Header from "../components/auth/Header";
import UserData from "../components/auth/profile/UserData";

import classes from "../components/auth/profile/ChooseAcademyEntitiesForm.module.css";

const Profile = () => {
  const loaderData = useLoaderData();
  const [user, setUser] = useState(loaderData.user);

  return (
    <>
      <Header heading="Panel użytkownika" className={classes.heading} />
      <Container className={classes["flex-start"]}>
        <div className={classes.container}>
          <Header heading="Przypisz się do uczelni, wydziału i kierunku" />
          <ChooseAcademyEntitiesForm user={user} setUser={setUser} />
        </div>
        <div className={classes.container}>
          <Header heading="Dostępne akcje" />
          <ProfileActions />
        </div>
        <div className={classes.container}>
          <Header heading="Twoje dane" />
          <UserData user={user} />
        </div>
      </Container>
    </>
  );
};

export default Profile;
