import classes from "./UserData.module.css";

const UserData = ({ user }) => {
  return (
    <>
      <p className={classes.p}>
        Nazwa użytkownika: <span>{user.userName}</span>
      </p>
      <p className={classes.p}>
        Email: <span>{user.email}</span>
      </p>
      <p className={classes.p}>
        Numer telefonu:{" "}
        <span>{!!user.phoneNumber ? user.phoneNumber : "Nie podano"}</span>
      </p>

      <p className={classes.p}>
        Email potwierdzony: <span>{user.emailConfirmed ? "TAK" : "NIE"}</span>
      </p>
      <p className={classes.p}>
        Uczelnia: <span>{user.universityName}</span>
      </p>
      <p className={classes.p}>
        Wydział: <span>{user.facultyName}</span>
      </p>
      <p className={classes.p}>
        Kierunek: <span>{user.universitySubjectName}</span>
      </p>
    </>
  );
};

export default UserData;
