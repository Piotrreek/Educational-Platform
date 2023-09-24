import { redirect } from "react-router-dom";

export const forgotPassword = async (email) => {
  try {
    const response = await fetch(
      `${process.env.REACT_APP_BACKEND_URL}user/send-reset-password-link`,
      {
        method: "POST",
        body: JSON.stringify({ email: email }),
        headers: {
          "Content-Type": "application/json",
        },
      }
    );

    if (response.status === 404) {
      return { error: "Niepoprawny adres e-mail" };
    }

    if (!response.ok) {
      return redirect("/error");
    }

    return await response.json();
  } catch (_) {
    return redirect("/error");
  }
};

export const resetPassword = async (
  newPassword,
  confirmNewPassword,
  userId,
  token
) => {
  try {
    const response = await fetch(
      `${process.env.REACT_APP_BACKEND_URL}user/${userId}/reset-password?token=${token}`,
      {
        method: "POST",
        body: JSON.stringify({
          password: newPassword,
          confirmPassword: confirmNewPassword,
        }),
        headers: {
          "Content-Type": "application/json",
        },
      }
    );

    if (response.status === 400) {
      return await response.json();
    }

    if (!response.ok) {
      return redirect("/error");
    }
  } catch (_) {
    return redirect("/error");
  }
};
