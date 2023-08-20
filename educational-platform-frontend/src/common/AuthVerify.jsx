import { useEffect } from "react";
import { useLocation } from "react-router-dom";

import { retrieveDataFromToken } from "../utils/jwtUtils";

const AuthVerify = ({ logout }) => {
  const location = useLocation();

  useEffect(() => {
    const tokenData = retrieveDataFromToken();
    if (!tokenData) {
      return;
    }

    if (tokenData.exp * 1000 < Date.now()) {
      logout();
      return;
    }

    const timeout = setTimeout(() => {
      logout();
    }, tokenData.exp * 1000 - Date.now());

    return () => clearTimeout(timeout);
  }, [location, logout]);
};

export default AuthVerify;
