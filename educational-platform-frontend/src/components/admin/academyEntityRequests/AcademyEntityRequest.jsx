import Button from "../../ui/Button";

import classes from "./AcademyEntityRequests.module.css";

const AcademyEntityRequest = ({ request }) => {
  const courseSessionConverter = (courseSessionInEnglish) => {
    switch (courseSessionInEnglish) {
      case "First":
        return "Pierwszy";
      case "Second":
        return "Drugi";
      case "Third":
        return "Trzeci";
      case "Fourth":
        return "Czwarty";
      case "Fifth":
        return "Piąty";
      case "Sixth":
        return "Szósty";
      case "Seventh":
        return "Siódmy";
      case "Eighth":
        return "Ósmy";
      case "Nineth":
        return "Dziewiąty";
      case "Tenth":
        return "Dziesiąty";
      case "Eleventh":
        return "Jedenasty";
      default:
        return "Dwunasty";
    }
  };

  const subjectDegreeConverter = (subjectDegreeInEnglish) => {
    return courseSessionConverter(subjectDegreeInEnglish);
  };

  return (
    <div className={classes.request}>
      <p>
        Nazwa: <span>{request.entityName}</span>
      </p>
      {!!request.courseSession && (
        <p>
          Numer semestru:
          <span>{courseSessionConverter(request.courseSession)}</span>
        </p>
      )}
      {!!request.subjectDegree && (
        <p>
          Stopień kierunku:
          <span>{subjectDegreeConverter(request.subjectDegree)}</span>
        </p>
      )}
      {!!request.universityName && (
        <p>
          Uczelnia: <span>{request.universityName}</span>
        </p>
      )}
      {!!request.facultyName && (
        <p>
          Wydział: <span>{request.facultyName}</span>
        </p>
      )}
      {!!request.subjectName && (
        <p>
          Kierunek: <span>{request.subjectName}</span>
        </p>
      )}
      <p>
        Zgłaszający: <span>{request.requesterName}</span>
      </p>
      {!!request.additionalInformation && (
        <p>
          Dodatkowe informacje: <span>{request.additionalInformation}</span>
        </p>
      )}
      <div className={classes.actions}>
        <Button>
          <span>&#9989;</span>Zaakceptuj
        </Button>
        <Button>
          <span>&#10060;</span>Odrzuć
        </Button>
      </div>
    </div>
  );
};

export default AcademyEntityRequest;
