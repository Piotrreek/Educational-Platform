import { getToken } from "../utils/jwtUtils";
import { BackendError } from "../utils/errors";
import { handleResponse } from "../utils/responseUtils";

export const createUniversitySubjectAction = async ({ request }) => {
  const formData = await request.formData();

  try {
    return await createUniversitySubject(
      formData.get("facultyId"),
      formData.get("subjectDegree"),
      formData.get("subjectName")
    );
  } catch (_) {
    return BackendError;
  }
};

export const createUniversitySubject = async (
  facultyId,
  subjectDegree,
  subjectName
) => {
  const response = await fetch(
    `${process.env.REACT_APP_BACKEND_URL}academy/subject`,
    {
      method: "POST",
      body: JSON.stringify({
        subjectName: subjectName,
        subjectDegree: subjectDegree,
        facultyId: facultyId,
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
