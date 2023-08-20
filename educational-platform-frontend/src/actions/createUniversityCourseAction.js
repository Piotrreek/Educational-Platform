import { getToken } from "../utils/jwtUtils";
import { BackendError } from "../utils/errors";
import { handleResponse } from "../utils/responseUtils";

export const createUniversityCourseAction = async ({request}) => {
    const formData = await request.formData();

  try {
    const response = await fetch(
      `${process.env.REACT_APP_BACKEND_URL}academy/course`,
      {
        method: request.method,
        body: JSON.stringify({
          courseName: formData.get("courseName"),
          courseSession: formData.get("courseSession"),
          universitySubjectId: formData.get("subjectId"),
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
}