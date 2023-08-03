import { NavLink, useLocation, Form } from "react-router-dom";
import { useEffect, useState } from "react";
import classes from "./Nav.module.css";
import { getToken } from "../../utils/jwtUtils";

const Nav = () => {
  const [hamburgerClicked, setHamburgerClicked] = useState(false);
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
              className={({ isActive }) =>
                isActive ? classes.active : undefined
              }
              to="/dsda"
              end
            >
              Link 1
            </NavLink>
          </li>
          <li>
            <NavLink
              to="/abc"
              className={({ isActive }) =>
                isActive ? classes.active : undefined
              }
              end
            >
              Link 2
            </NavLink>
          </li>
          <li>
            <NavLink
              to="/abc"
              className={({ isActive }) =>
                isActive ? classes.active : undefined
              }
              end
            >
              Link 3
            </NavLink>
          </li>
          <li>
            <NavLink
              to="/abc"
              className={({ isActive }) =>
                isActive ? classes.active : undefined
              }
              end
            >
              Link 4
            </NavLink>
          </li>
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
            <li>
              <Form action="/logout" method="POST">
                <button className={classes.logoutBtn} type="submit">Wyloguj</button>
              </Form>
            </li>
          )}
        </div>
      </ul>
    </nav>
  );
};

export default Nav;
