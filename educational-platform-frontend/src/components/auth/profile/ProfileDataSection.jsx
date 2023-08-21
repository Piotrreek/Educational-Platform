import classes from "./ProfileContent.module.css";

const ProfileDataSection = ({ user }) => {
  return (
    <section className={classes["profile-data"]}>
      <h2>Twoje dane</h2>
      <p>
        Nazwa użytkownika:<span>{user.userName}</span>
      </p>
      <p>
        E-mail:<span>{user.email}</span>
      </p>
      <p>
        Numer telefonu:
        <span>{!!user.phoneNumber ? user.phoneNumber : "Nie podano"}</span>
      </p>
      <p>
        E-mail potwierdzony:<span>{user.emailConfirmed ? "TAK" : "NIE"}</span>
      </p>
      <p>
        Uczelnia:
        <span>
          {!!user.universityName ? user.universityName : "Nie wybrano"}
        </span>
      </p>
      <p>
        Wydział:
        <span>{!!user.facultyName ? user.facultyName : "Nie wybrano"}</span>
      </p>
      <p>
        Kierunek:
        <span>
          {!!user.universitySubjectName
            ? user.universitySubjectName
            : "Nie wybrano"}
        </span>
      </p>
    </section>
  );
};

export default ProfileDataSection;
