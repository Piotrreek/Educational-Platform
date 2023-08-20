import { RouterProvider, createBrowserRouter } from "react-router-dom";
import useAuth from "./hooks/useAuth";

import Layout from "./pages/Layout";
import Home from "./pages/Home";
import Login from "./pages/Login";
import Register from "./pages/Register";
import AddDidacticMaterial from "./pages/AddDidacticMaterial";
import DidacticMaterials from "./pages/DidacticMaterials";
import ConfirmAccount from "./pages/ConfirmAccount";
import DidacticMaterial from "./pages/DidacticMaterial";
import Admin from "./pages/Admin";
import CreateUniversity from "./pages/CreateUniversity";
import CreateFaculty from "./pages/CreateFaculty";
import CreateUniversitySubject from "./pages/CreateUniversitySubject";
import CreateUniversityCourse from "./pages/CreateUniversityCourse";
import Profile from "./pages/Profile";

import { createUniversityAction } from "./actions/createUniversityAction";
import { createFacultyAction } from "./actions/createFacultyAction";
import { createUniversitySubjectAction } from "./actions/createUniversitySubjectAction";
import { createMaterialOpinionAction } from "./actions/createMaterialOpinionAction";
import { loginAction } from "./actions/loginAction";
import { registerAction } from "./actions/registerAction";
import { createDidacticMaterialAction } from "./actions/createDidacticMaterialAction";
import { logoutAction } from "./actions/logoutAction";

import { loadUniversityEntities } from "./loaders/loadUniversityEntities";
import { createUniversityCourseAction } from "./actions/createUniversityCourseAction";

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
        {
          path: "confirm-account/:userId",
          element: <ConfirmAccount />,
        },
        {
          path: "/didactic-material",
          children: [
            {
              index: true,
              element: <DidacticMaterials />,
              loader: loadUniversityEntities,
            },
            {
              path: "create",
              element: <AddDidacticMaterial />,
              loader: loadUniversityEntities,
              action: createDidacticMaterialAction,
              shouldRevalidate: () => false,
            },
            {
              path: ":id",
              element: <DidacticMaterial />,
              action: createMaterialOpinionAction,
            },
          ],
        },
        {
          path: "/logout",
          action: logoutAction({ logout }),
        },
        {
          path: "/profile",
          element: <Profile />,
          loader: loadUniversityEntities,
          shouldRevalidate: () => false,
        },
        {
          path: "/admin",
          id: "admin",
          element: <Admin />,
          loader: loadUniversityEntities,
          shouldRevalidate: () => false,
          children: [
            {
              path: "create-university",
              element: <CreateUniversity />,
              action: createUniversityAction,
            },
            {
              path: "create-faculty",
              element: <CreateFaculty />,
              action: createFacultyAction,
            },
            {
              path: "create-university-subject",
              element: <CreateUniversitySubject />,
              action: createUniversitySubjectAction,
            },
            {
              path: "create-university-course",
              element: <CreateUniversityCourse />,
              action: createUniversityCourseAction,
            },
          ],
        },
      ],
    },
  ]);

  return <RouterProvider router={router} />;
};

export default App;
