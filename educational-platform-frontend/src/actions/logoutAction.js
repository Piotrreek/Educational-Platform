import { redirect } from "react-router-dom";

export const logoutAction =
  ({ logout }) =>
  () => {
    logout();

    return redirect("/login");
  };
