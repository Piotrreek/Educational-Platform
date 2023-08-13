import { useEffect, useState } from "react";
import { getRole } from "../utils/jwtUtils";
import AuthContext from "./AuthContext";

const AuthProvider = ({ children }) => {
  const [claims, setClaims] = useState({ role: null, isLoggedIn: false });

  useEffect(() => {
    const role = getRole();

    if (!!role) {
      setClaims({ role: role, isLoggedIn: true });
    }
  }, []);

  return (
    <AuthContext.Provider value={{ claims, setClaims }}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthProvider;
