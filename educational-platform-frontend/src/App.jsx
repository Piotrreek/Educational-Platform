import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Layout from "./pages/Layout";
import Home from "./pages/Home";
import Login from "./pages/Login";
import Register from "./pages/Register";
import { loginAction } from "./actions/loginAction";
import { registerAction } from "./actions/registerAction";
import useAuth from "./hooks/useAuth";
import logoutAction from "./actions/logoutAction";

const App = () => {
  const { login, logout } = useAuth();

  const router = createBrowserRouter([
    {
      path: "/",
      element: <Layout />,
      children: [
        { index: true, element: <Home /> },
        {
          path: "/login",
          element: <Login />,
          action: loginAction({ login }),
        },
        {
          path: "/register",
          element: <Register />,
          action: registerAction,
        },
        { path: "/logout", action: logoutAction({ logout }) },
      ],
    },
  ]);

  return <RouterProvider router={router} />;
};

export default App;
