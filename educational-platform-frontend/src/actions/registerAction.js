export const registerAction = async ({ request }) => {
  const formData = await request.formData();
  const body = {
    username: formData.get("username"),
    email: formData.get("email"),
    password: formData.get("password"),
    confirmPassword: formData.get("confirm-password"),
    requestedRoleName: formData.get("role"),
  };

  try {
    const response = await fetch(
      `${process.env.REACT_APP_BACKEND_URL}user/register`,
      {
        method: request.method,
        body: JSON.stringify(body),
        credentials: "include",
        headers: {
          "Content-Type": "application/json",
        },
      }
    );

    if (response.status === 400) {
      return { message: (await response.json()).message };
    }

    if (!response.ok) {
      return {
        message:
          "Nie udało się przetworzyć zapytania spróbuj ponownie za chwilę",
      };
    }

    return { isSuccess: true };
  } catch (error) {
    return {
      message: "Serwer nie odpowiada, spróbuj ponownie za chwilę",
    };
  }
};
