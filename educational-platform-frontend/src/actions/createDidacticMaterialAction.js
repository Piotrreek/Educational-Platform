import { redirect } from "react-router-dom";
import { getToken } from "../utils/jwtUtils";

export const createDidacticMaterialAction = async ({ request }) => {
  const data = await request.formData();
  try {
    const response = await fetch(
      `${process.env.REACT_APP_BACKEND_URL}didactic-material`,
      {
        method: request.method,
        body: data,
        credentials: "include",
        headers: {
          Authorization: `Bearer ${getToken()}`,
        },
      }
    );

    if (response.status === 401) {
      return redirect("/login");
    }

    if (!response.ok) {
      return {
        error:
          "Wystąpił błąd po stronie serwera, spróbuj ponownie za kilka minut",
      };
    }

    return { isSuccess: true };
  } catch (error) {
    return {
      error:
        "Wystąpił błąd po stronie serwera, spróbuj ponownie za kilka minut",
    };
  }
};
