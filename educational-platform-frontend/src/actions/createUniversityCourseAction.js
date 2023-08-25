import { getToken } from "../utils/jwtUtils";
import { BackendError } from "../utils/errors";
import { handleResponse } from "../utils/responseUtils";

export const createUniversityCourseAction = async ({ request }) => {
  const formData = await request.formData();

  try {
    return await createUniversityCourse(
      formData.get("subjectId"),
      formData.get("courseSession"),
      formData.get("courseName")
    );
  } catch (_) {
    return BackendError;
  }
};

export const createUniversityCourse = async (
  subjectId,
  courseSession,
  courseName
) => {
  const response = await fetch(
    `${process.env.REACT_APP_BACKEND_URL}academy/course`,
    {
      method: "POST",
      body: JSON.stringify({
        courseName: courseName,
        courseSession: courseSession,
        universitySubjectId: subjectId,
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
