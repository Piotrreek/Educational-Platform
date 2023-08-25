import { redirect } from "react-router-dom";
import { getToken } from "../utils/jwtUtils";

export const academyEntityRequestsLoader = async () => {
  try {
    const response = await fetch(
      `${process.env.REACT_APP_BACKEND_URL}academy/request`,
      {
        method: "GET",
        credentials: "include",
        headers: {
          Authorization: `Bearer ${getToken()}`,
        },
      }
    );

    if (response.status === 401) {
      return redirect("/login");
    }

    const data = await response.json();

    return { requestsGroupedByType: data };
  } catch (_) {}
};
