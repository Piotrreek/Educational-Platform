import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Layout from "./pages/Layout";
import Home from "./pages/Home";
import Login from "./pages/Login";
import Register from "./pages/Register";
import { loginAction } from "./actions/loginAction";
import { registerAction } from "./actions/registerAction";
import AuthVerify from "./common/AuthVerify";
import { removeToken } from "./utils/jwtUtils";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Layout />,
    children: [
      { index: true, element: <Home /> },
      {
        path: "/login",
        element: <Login />,
        action: loginAction,
      },
      {
        path: "/register",
        element: <Register />,
        action: registerAction,
      },
    ],
  },
]);
const App = () => {
  return (
    <>
      <RouterProvider router={router}>
        <AuthVerify logout={removeToken} />
      </RouterProvider>
    </>
  );
};

export default App;
