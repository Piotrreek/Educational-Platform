import { getToken } from "../utils/jwtUtils";
import { handleResponse } from "../utils/responseUtils";
import { BackendError } from "../utils/errors";

export const createFacultyAction = async ({ request }) => {
  const formData = await request.formData();

  try {
    return await createFaculty(
      formData.get("universityId"),
      formData.get("facultyName")
    );
  } catch (_) {
    return BackendError;
  }
};

export const createFaculty = async (universityId, facultyName) => {
  const response = await fetch(
    `${process.env.REACT_APP_BACKEND_URL}academy/faculty`,
    {
      method: "POST",
      body: JSON.stringify({
        universityId: universityId,
        facultyName: facultyName,
      }),
      credentials: "include",
      headers: {
        Authorization: `Bearer ${getToken()}`,
        "Content-Type": "application/json",
      },
    }
  );

  return await handleResponse(response);
};
