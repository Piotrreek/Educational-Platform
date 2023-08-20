import { getToken } from "../utils/jwtUtils";
import { BackendError } from "../utils/errors";
import { handleResponse } from "../utils/responseUtils";

export const createUniversityAction = async ({ request }) => {
  const formData = await request.formData();

  try {
    const response = await fetch(
      `${process.env.REACT_APP_BACKEND_URL}academy/university`,
      {
        method: request.method,
        body: JSON.stringify({
          universityName: formData.get("universityName"),
        }),
        credentials: "include",
        headers: {
          Authorization: `Bearer ${getToken()}`,
          "Content-Type": "application/json",
        },
      }
    );

    return await handleResponse(response);
  } catch (_) {
    return BackendError;
  }
};
