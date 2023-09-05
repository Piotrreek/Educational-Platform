import { redirect } from "react-router-dom";
import { getToken } from "../utils/jwtUtils";

export const rateContent = async (endpoint, contentId, method, body) => {
  try {
    const response = await fetch(
      `${process.env.REACT_APP_BACKEND_URL}${endpoint}/${contentId}/rate`,
      {
        method: method,
        credentials: "include",
        headers: {
          Authorization: `Bearer ${getToken()}`,
          "Content-Type": "application/json",
        },
        body: JSON.stringify(body),
      }
    );

    if (!response.ok) {
      return redirect("/error");
    }

    return await response.json();
  } catch (_) {
    return redirect("/error");
  }
};
