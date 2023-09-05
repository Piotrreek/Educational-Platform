import { redirect } from "react-router-dom";
import { getToken } from "../utils/jwtUtils";

export const materialLoader = async ({ params }) => {
  try {
    const response = await fetch(
      `${process.env.REACT_APP_BACKEND_URL}didactic-material/${params.id}`,
      {
        credentials: "include",
        headers: {
          Authorization: `Bearer ${getToken()}`,
        },
      }
    );

    if (response.status === 404) {
      return redirect("/not-found");
    }

    if (!response.ok) {
      return redirect("/error");
    }

    return await response.json();
  } catch (_) {
    return redirect("/error");
  }
};
