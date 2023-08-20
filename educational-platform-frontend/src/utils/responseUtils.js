import { redirect } from "react-router-dom";
import { BackendError } from "./errors";

export const handleResponse = async (response) => {
  if (response.status === 401) {
    return redirect("/login");
  }

  if (response.status === 403) {
    // redirect to forbidden page
  }

  if (response.status === 400) {
    const responseData = await response.json();

    if (!!responseData.message) {
      return { error: responseData.message };
    }
  }

  if (!response.ok) {
    return BackendError;
  }

  return { isSuccess: true };
};
