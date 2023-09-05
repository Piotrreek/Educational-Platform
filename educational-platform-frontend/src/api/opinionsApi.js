import { redirect } from "react-router-dom";
import { getToken } from "../utils/jwtUtils";

export const createOpinion = async (endpoint, id, body) => {
  try {
    const response = await fetch(
      `${process.env.REACT_APP_BACKEND_URL}${endpoint}/${id}/opinion`,
      {
        method: "POST",
        credentials: "include",
        body: JSON.stringify(body),
        headers: {
          Authorization: `Bearer ${getToken()}`,
          "Content-Type": "application/json",
        },
      }
    );

    if (!response.ok) {
      return redirect("/error");
    }
  } catch (_) {
    return redirect("/error");
  }
};

export const getOpinions = async (endpoint, id) => {
  try {
    const response = await fetch(
      `${process.env.REACT_APP_BACKEND_URL}${endpoint}/${id}/opinion`,
      {
        credentials: "include",
        headers: {
          Authorization: `Bearer ${getToken()}`,
        },
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
