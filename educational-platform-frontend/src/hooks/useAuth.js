import { useContext } from "react";
import AuthContext from "../store/AuthContext";
import { getRole, removeToken, saveToken } from "../utils/jwtUtils";

const useAuth = () => {
  const authCtx = useContext(AuthContext);

  const logout = () => {
    removeToken();
    authCtx.setClaims({ role: null });
  };

  const login = (token) => {
    saveToken(token);
    const role = getRole();
    if (!!role) {
      authCtx.setClaims({ role: role });
    }
  };

  return {
    ctx: authCtx,
    logout,
    login,
  };
};

export default useAuth;
