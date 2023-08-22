import { getToken } from "../utils/jwtUtils";

export const userLoader = async () => {
  try {
    const response = await fetch(`${process.env.REACT_APP_BACKEND_URL}user`, {
      credentials: "include",
      headers: {
        Authorization: `Bearer ${getToken()}`,
      },
    });

    if (!response.ok) {
      return null;
    }

    const data = await response.json();

    return { user: data };
  } catch (e) {}
};
