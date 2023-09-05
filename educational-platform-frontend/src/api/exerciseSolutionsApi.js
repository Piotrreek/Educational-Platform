import { redirect } from "react-router-dom";
import { getToken } from "../utils/jwtUtils";

export const addNewSolution = async (file, exerciseId) => {
  const formData = new FormData();
  formData.append("solutionFile", file);

  try {
    const response = await fetch(
      `${process.env.REACT_APP_BACKEND_URL}exercise/${exerciseId}/solution`,
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

export const getSolutions = async (exerciseId) => {
  try {
    const response = await fetch(
      `${process.env.REACT_APP_BACKEND_URL}exercise/${exerciseId}/solution`,
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

export const rateSolution = async (solutionId, method, body) => {
  try {
    const response = await fetch(
      `${process.env.REACT_APP_BACKEND_URL}exercise/solution/${solutionId}/rate`,
      {
        method: method,
        credentials: "include",
        headers: {
          Authorization: `Bearer ${getToken()}`,
          "Content-Type": "application/json",
        },
        body: JSON.stringify(body),
      }
    );

    if (!response.ok) {
      return redirect("/error");
    }
  } catch (_) {
    return redirect("/error");
  }
};

export const getSolutionRatingObject = async (solutionId) => {
  try {
    const response = await fetch(
      `${process.env.REACT_APP_BACKEND_URL}exercise/solution/${solutionId}/rate`,
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
