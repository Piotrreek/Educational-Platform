import { useRouteLoaderData } from "react-router-dom";
import { useState } from "react";
import useInput from "../../hooks/useInput";
import useAcademyEntitiesReset from "../../hooks/useAcademyEntitiesReset";
import useCurrentAcademyEntitiesOptions from "../../hooks/useCurrentAcademyEntitiesOptions";

import Input from "../ui/Input";
import Actions from "../auth/Actions";
import Button from "../ui/Button";
import TextArea from "../ui/TextArea";
import Select from "../ui/Select";

import { notEmpty } from "../../utils/validators";
import { getToken } from "../../utils/jwtUtils";
import { BackendError } from "../../utils/errors";
import { AcademyEntityTypes } from "../../utils/academyEntityTypes";

import classes from "../ui/Form.module.css";

const CreateAcademyEntityRequestForm = ({ style, type, onClose }) => {
  const loaderData = useRouteLoaderData("index");
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState();
  const [isSuccess, setIsSuccess] = useState(false);
  const [additionalInfo, setAdditionalInfo] = useState();

  const { value: subjectDegree, onChange: onSubjectDegreeChange } = useInput(
    notEmpty,
    "First"
  );

  const { value: courseSession, onChange: onCourseSessionChange } = useInput(
    notEmpty,
    "First"
  );

  const {
    value: entityName,
    isValid: entityNameIsValid,
    error: entityNameError,
    hasError: entityNameHasError,
    onBlur: onEntityNameBlur,
    onChange: onEntityNameChange,
  } = useInput(notEmpty);

  const {
    value: universityId,
    onChange: onUniversityIdChange,
    onBlur: onUniversityIdBlur,
    hasError: universityIdHasError,
    error: universityIdError,
    isValid: universityIdIsValid,
  } = useInput(notEmpty);

  const {
    value: facultyId,
    onChange: onFacultyIdChange,
    onBlur: onFacultyIdBlur,
    hasError: facultyIdHasError,
    error: facultyIdError,
    isValid: facultyIdIsValid,
    reset: resetFacultyId,
  } = useInput(notEmpty);

  const {
    value: subjectId,
    onChange: onSubjectIdChange,
    onBlur: onSubjectIdBlur,
    hasError: subjectIdHasError,
    error: subjectIdError,
    isValid: subjectIdIsValid,
    reset: resetSubjectId,
  } = useInput(notEmpty);

  useAcademyEntitiesReset(
    null,
    resetSubjectId,
    resetFacultyId,
    universityId,
    facultyId
  );

  const { universityOptions, facultyOptions, subjectOptions } =
    useCurrentAcademyEntitiesOptions(
      loaderData.universityEntities,
      universityId,
      facultyId
    );

  const formStateIsValid = () => {
    switch (type) {
      case AcademyEntityTypes.University:
        return entityNameIsValid;
      case AcademyEntityTypes.Faculty:
        return entityNameIsValid && universityIdIsValid;
      case AcademyEntityTypes.UniversitySubject:
        return entityNameIsValid && universityIdIsValid && facultyIdIsValid;
      case AcademyEntityTypes.UniversityCourse:
        return (
          entityNameIsValid &&
          universityIdIsValid &&
          facultyIdIsValid &&
          subjectIdIsValid
        );
      default:
        return false;
    }
  };

  const onSubmit = async (e) => {
    e.preventDefault();

    const body = JSON.stringify({
      entityType: type,
      entityName: entityName,
      additionalInformation: additionalInfo,
      universitySubjectDegree: subjectDegree,
      universityCourseSession: courseSession,
      universityId: !!universityId ? universityId : null,
      facultyId: !!facultyId ? facultyId : null,
      universitySubjectId: !!subjectId ? subjectId : null,
    });
    setSubmitting(true);
    try {
      const response = await fetch(
        `${process.env.REACT_APP_BACKEND_URL}academy/request`,
        {
          method: "POST",
          credentials: "include",
          body: body,
          headers: {
            Authorization: `Bearer ${getToken()}`,
            "Content-Type": "application/json",
          },
        }
      );

      if (!response.ok) {
        setError(BackendError);
        setSubmitting(false);

        setTimeout(() => {
          setError();
        }, 3000);
      }

      setIsSuccess(true);
      setTimeout(() => {
        onClose();
      }, 2000);
    } catch (_) {}
    setSubmitting(false);
  };

  const courseSessionOptions = [
    { value: "First", text: "1" },
    { value: "Second", text: "2" },
    { value: "Third", text: "3" },
    { value: "Fourth", text: "4" },
    { value: "Fifth", text: "5" },
    { value: "Sixth", text: "6" },
    { value: "Seventh", text: "7" },
    { value: "Eighth", text: "8" },
    { value: "Nith", text: "9" },
    { value: "Tenth", text: "10" },
    { value: "Eleventh", text: "11" },
    { value: "Twelfth", text: "12" },
  ];

  const subjectDegreeOptions = [
    { value: "First", text: "Pierwszy stopień" },
    { value: "Second", text: "Drugi stopień" },
  ];

  return (
    <form className={classes.form} style={style} onSubmit={onSubmit}>
      {isSuccess && <p className={classes.success}>Pomyślnie wysłano prośbę</p>}
      {!!error && <p className={classes.error}>{error}</p>}
      <Input
        id="entityName"
        name="entityName"
        label="Nazwa"
        value={entityName}
        error={entityNameError}
        hasError={entityNameHasError}
        onBlur={onEntityNameBlur}
        onChange={onEntityNameChange}
      />
      {type === AcademyEntityTypes.UniversitySubject && (
        <Select
          id="subjectDegree"
          name="subjectDegree"
          label="Stopień studiów"
          options={subjectDegreeOptions}
          value={subjectDegree}
          onChange={onSubjectDegreeChange}
        />
      )}
      {type === AcademyEntityTypes.UniversityCourse && (
        <Select
          id="courseSession"
          name="courseSession"
          label="Wybierz semestr"
          options={courseSessionOptions}
          value={courseSession}
          onChange={onCourseSessionChange}
        />
      )}
      {(type === AcademyEntityTypes.Faculty ||
        type === AcademyEntityTypes.UniversityCourse ||
        type === AcademyEntityTypes.UniversitySubject) && (
        <Select
          id="universityId"
          name="universityId"
          label="Uczelnia"
          options={universityOptions}
          onChange={onUniversityIdChange}
          onBlur={onUniversityIdBlur}
          value={universityId}
          hasError={universityIdHasError}
          error={universityIdError}
        />
      )}
      {(type === AcademyEntityTypes.UniversitySubject ||
        type === AcademyEntityTypes.UniversityCourse) && (
        <Select
          id="facultyId"
          name="facultyId"
          label="Wydział"
          options={facultyOptions}
          onChange={onFacultyIdChange}
          onBlur={onFacultyIdBlur}
          value={facultyId}
          disabled={!universityId}
          hasError={facultyIdHasError}
          error={facultyIdError}
        />
      )}
      {type === AcademyEntityTypes.UniversityCourse && (
        <Select
          id="subjectId"
          name="subjectId"
          label="Kierunek"
          options={subjectOptions}
          onChange={onSubjectIdChange}
          onBlur={onSubjectIdBlur}
          value={subjectId}
          disabled={!universityId || !facultyId}
          hasError={subjectIdHasError}
          error={subjectIdError}
        />
      )}
      {type !== AcademyEntityTypes.University && (
        <TextArea
          id="additionalInfo"
          name="additionalInfo"
          label="Dodatkowe informacje (gdybyś chciał/chciała dodać coś jeszcze, na przykład kierunek pod wydziałem, który administrator utworzy)"
          value={additionalInfo}
          onChange={(e) => setAdditionalInfo(e.target.value)}
        />
      )}
      <Actions className={classes["actions-right"]}>
        <Button disabled={!formStateIsValid()}>
          {submitting ? "Wysyłam..." : "Wyślij"}
        </Button>
      </Actions>
    </form>
  );
};

export default CreateAcademyEntityRequestForm;
