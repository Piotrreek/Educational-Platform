import { getToken } from "../utils/jwtUtils";
import { handleResponse } from "../utils/responseUtils";
import { BackendError } from "../utils/errors";

export const createFacultyAction = async ({ request }) => {
  const formData = await request.formData();

  try {
    const response = await fetch(
      `${process.env.REACT_APP_BACKEND_URL}academy/faculty`,
      {
        method: request.method,
        body: JSON.stringify({
          universityId: formData.get("universityId"),
          facultyName: formData.get("facultyName"),
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
