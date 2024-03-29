import { Form, NavLink, useLocation } from "react-router-dom";
import { useEffect, useState } from "react";
import classes from "./Nav.module.css";
import { getToken } from "../../utils/jwtUtils";
import useAuth from "../../hooks/useAuth";

const Nav = () => {
  const [hamburgerClicked, setHamburgerClicked] = useState(false);
  const { ctx } = useAuth();
  const location = useLocation();

  const hamburgerContainerClickHandler = () => {
    setHamburgerClicked((prev) => !prev);
  };

  useEffect(() => {
    setHamburgerClicked(false);
  }, [location]);

  return (
    <nav>
      <div
        onClick={hamburgerContainerClickHandler}
        className={classes["hamburger-container"]}
      >
        <div
          className={`${classes.hamburger} ${
            hamburgerClicked ? classes.vertical : classes.horizontal
          }`}
        ></div>
      </div>
      <ul
        className={`${classes["nav-list"]} ${
          !hamburgerClicked ? classes["hide"] : ""
        }`}
      >
        <div>
          <li>
            <NavLink
              className={({ isActive }) =>
                isActive ? classes.active : undefined
              }
              to="/"
              end
            >
              Strona główna
            </NavLink>
          </li>
          <li>
            <NavLink
              to="/didactic-material"
              className={({ isActive }) =>
                isActive ? classes.active : undefined
              }
              end
            >
              Materiały
            </NavLink>
          </li>
          <li>
            <NavLink
              to="/exercise"
              className={({ isActive }) =>
                isActive ? classes.active : undefined
              }
              end
            >
              Ćwiczenia
            </NavLink>
          </li>
          {ctx.claims.isLoggedIn && (
            <>
              <li>
                <NavLink
                  className={({ isActive }) =>
                    isActive ? classes.active : undefined
                  }
                  to="/didactic-material/create"
                  end
                >
                  Stwórz materiał
                </NavLink>
              </li>
              <li>
                <NavLink
                  className={({ isActive }) =>
                    isActive ? classes.active : undefined
                  }
                  to="/exercise/create"
                  end
                >
                  Stwórz ćwiczenie
                </NavLink>
              </li>
            </>
          )}

          {ctx.claims.role === "Administrator" && (
            <li>
              <NavLink
                to="/admin"
                className={({ isActive }) =>
                  isActive ? classes.active : undefined
                }
                end
              >
                Panel Admina
              </NavLink>
            </li>
          )}
        </div>
        <div className={classes["nav-list__auth"]}>
          {!getToken() ? (
            <>
              <li>
                <NavLink
                  to="/login"
                  className={({ isActive }) =>
                    isActive ? classes.active : undefined
                  }
                  end
                >
                  Zaloguj
                </NavLink>
              </li>
              <li>
                <NavLink
                  to="/register"
                  className={({ isActive }) =>
                    isActive ? classes.active : undefined
                  }
                  end
                >
                  Zarejestruj
                </NavLink>
              </li>
            </>
          ) : (
            <>
              {ctx.claims.isLoggedIn && (
                <li>
                  <NavLink
                    to="/profile"
                    className={({ isActive }) =>
                      isActive ? classes.active : undefined
                    }
                    end
                  >
                    Profil
                  </NavLink>
                </li>
              )}

              <li>
                <Form action="/logout" method="POST">
                  <button className={classes.logoutBtn}>Wyloguj</button>
                </Form>
              </li>
            </>
          )}
        </div>
      </ul>
    </nav>
  );
};

export default Nav;
