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
import AcademyEntityRequests from "./pages/AcademyEntityRequests";
import CreateExercise from "./pages/CreateExercise";
import Exercise from "./pages/Exercise";
import Exercises from "./pages/Exercises";
import ForgotPassword from "./pages/ForgotPassword";
import ResetPassword from "./pages/ResetPassword";
import NotFound from "./pages/NotFound";
import Error from "./pages/Error";

import { createUniversityAction } from "./actions/createUniversityAction";
import { createFacultyAction } from "./actions/createFacultyAction";
import { createUniversitySubjectAction } from "./actions/createUniversitySubjectAction";
import { loginAction } from "./actions/loginAction";
import { registerAction } from "./actions/registerAction";
import { createDidacticMaterialAction } from "./actions/createDidacticMaterialAction";
import { logoutAction } from "./actions/logoutAction";

import { loadUniversityEntities } from "./loaders/loadUniversityEntities";
import { userLoader } from "./loaders/userLoader";
import { createUniversityCourseAction } from "./actions/createUniversityCourseAction";
import { academyEntityRequestsLoader } from "./loaders/academyEntityRequestsLoader";
import { exerciceLoader } from "./loaders/exerciseLoader";
import { materialLoader } from "./loaders/materialLoader";

const App = () => {
  const { login, logout } = useAuth();

  const router = createBrowserRouter([
    {
      id: "index",
      path: "/",
      element: <Layout />,
      errorElement: <Error />,
      loader: loadUniversityEntities,
      shouldRevalidate: () => false,
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
          path: "forgot-password",
          element: <ForgotPassword />,
        },
        {
          path: "reset-password/:userId",
          element: <ResetPassword />,
        },
        {
          path: "/didactic-material",
          children: [
            {
              index: true,
              element: <DidacticMaterials />,
            },
            {
              path: "create",
              element: <AddDidacticMaterial />,
              action: createDidacticMaterialAction,
            },
            {
              path: ":id",
              element: <DidacticMaterial />,
              loader: materialLoader,
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
          loader: userLoader,
          shouldRevalidate: () => false,
        },
        {
          path: "/admin",
          id: "admin",
          element: <Admin />,
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
            {
              path: "entity-request",
              element: <AcademyEntityRequests />,
              loader: academyEntityRequestsLoader,
            },
          ],
        },
        {
          path: "exercise/create",
          element: <CreateExercise />,
        },
        {
          path: "exercise/:id",
          element: <Exercise />,
          loader: exerciceLoader,
        },
        {
          path: "exercise",
          element: <Exercises />,
        },
        {
          path: "*",
          element: <NotFound />,
        },
      ],
    },
  ]);

  return <RouterProvider router={router} />;
};

export default App;
