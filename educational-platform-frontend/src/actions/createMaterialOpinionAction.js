import { redirect } from "react-router-dom";
import { getToken } from "../utils/jwtUtils";

export const createMaterialOpinionAction = async ({ request, params }) => {
  const id = params.id;
  const data = await request.formData();

  try {
    const response = await fetch(
      `${process.env.REACT_APP_BACKEND_URL}didactic-material/opinion`,
      {
        method: request.method,
        body: JSON.stringify({
          didacticMaterialId: id,
          opinion: data.get("opinion"),
        }),
        credentials: "include",
        headers: {
          Authorization: `Bearer ${getToken()}`,
        },
      }
    );

    if (response.status === 401) {
      return redirect("login");
    }

    if (!response.ok) {
      return {
        error:
          "Wystąpił błąd po stronie serwera, spróbuj ponownie za kilka minut",
      };
    }

    return { isSuccess: true };
  } catch (_) {
    return {
      error:
        "Wystąpił błąd po stronie serwera, spróbuj ponownie za kilka minut",
    };
  }
};
