import { useLoaderData } from "react-router-dom";
import { useState } from "react";

import ProfileContent from "../components/auth/profile/ProfileContent";

const Profile = () => {
  const loaderData = useLoaderData();
  const [user, setUser] = useState(loaderData.user);

  return <ProfileContent user={user} setUser={setUser} />;
};

export default Profile;
