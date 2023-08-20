import { Link } from "react-router-dom";

import classes from "./AdminAction.module.css";

const AdminAction = ({ route, label }) => {
  return (
    <li className={classes['admin-action']}>
      <Link to={route}>{label}</Link>
    </li>
  );
};

export default AdminAction;
