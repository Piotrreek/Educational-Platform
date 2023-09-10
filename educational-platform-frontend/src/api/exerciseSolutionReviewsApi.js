import { getToken } from "../utils/jwtUtils";
import { redirect } from "react-router-dom";

export const addNewReview = async (file, content, solutionId) => {
  const formData = new FormData();
  formData.append("reviewContent", content);
  formData.append("reviewFile", file);

  try {
    const response = await fetch(
      `${process.env.REACT_APP_BACKEND_URL}exercise/solution/${solutionId}/review`,
      {
        method: "POST",
        credentials: "include",
        body: formData,
        headers: {
          Authorization: `Bearer ${getToken()}`,
        },
      }
    );

    if (response.status === 401) {
      return redirect("/login");
    }

    if (!response.ok) {
      return redirect("/error");
    }
  } catch (_) {
    return redirect("/error");
  }
};

export const getReviews = async (solutionId) => {
  try {
    const response = await fetch(
      `${process.env.REACT_APP_BACKEND_URL}exercise/solution/${solutionId}/review`,
      {
        credentials: "include",
        headers: {
          Authorization: `Bearer ${getToken()}`,
        },
      }
    );

    if (!response.ok) {
      return redirect("/error");
    }

    return await response.json();
  } catch (_) {
    return redirect("/error");
  }
};
