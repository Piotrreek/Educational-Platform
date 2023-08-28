import { getToken } from "../utils/jwtUtils";
import { BackendError } from "../utils/errors";
import { handleResponse } from "../utils/responseUtils";

export const createUniversityAction = async ({ request }) => {
  const formData = await request.formData();

  try {
    return await createUniversity(formData.get("universityName"));
  } catch (_) {
    return BackendError;
  }
};

export const createUniversity = async (universityName) => {
  const response = await fetch(
    `${process.env.REACT_APP_BACKEND_URL}academy/university`,
    {
      method: "POST",
      body: JSON.stringify({
        universityName: universityName,
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
