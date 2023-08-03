import { redirect } from "react-router-dom";

export const loginAction =
  ({ login }) =>
  async ({ request }) => {
    const formData = await request.formData();
    const body = {
      email: formData.get("email"),
      password: formData.get("password"),
    };

    const response = await fetch(
      `${process.env.REACT_APP_BACKEND_URL}user/login`,
      {
        method: request.method,
        body: JSON.stringify(body),
        credentials: "include",
        headers: {
          "Content-Type": "application/json",
        },
      }
    );

    if (response.status === 401) {
      return { message: "Podany login lub hasło jest nieprawidłowe" };
    }

    if (!response.ok) {
      return {
        message:
          "Nie udało się przetworzyć zapytania spróbuj ponownie za chwilę",
      };
    }

    const token = (await response.json()).token;
    login(token);

    return redirect("/");
  };
