import { redirect } from "react-router-dom";

const logoutAction =
  ({ logout }) =>
  () => {
    logout();
    
    return redirect("/");
  };

export default logoutAction;
