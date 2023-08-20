import AdminAction from "./AdminAction";

import classes from "./AdminActions.module.css";

const AdminActions = () => {
  return (
    <ul className={classes.list}>
      <AdminAction route="create-university" label="Stwórz uczelnię" />
      <AdminAction route="create-faculty" label="Stwórz wydział" />
      <AdminAction route="create-university-subject" label="Stwórz kierunek" />
      <AdminAction route="create-university-course" label="Stwórz przedmiot" />
    </ul>
  );
};

export default AdminActions;
