import { useState } from "react";
import { Form, useRouteLoaderData } from "react-router-dom";
import useInput from "../../../hooks/useInput";
import useCurrentAcademyEntitiesOptions from "../../../hooks/useCurrentAcademyEntitiesOptions";

import { notEmpty } from "../../../utils/validators";

import ActionDataMessage from "./ActionDataMessage";
import EntityNameInput from "./EntityNameInput";
import Select from "../../ui/Select";
import Button from "../../ui/Button";

import classes from "./CreateAcademyEntityForm.module.css";
import useAcademyEntitiesReset from "../../../hooks/useAcademyEntitiesReset";

const CreateUniversityCourseForm = () => {
  const [nameIsValid, setNameIsValid] = useState(false);
  const loaderData = useRouteLoaderData("index");

  const { value: courseSession, onChange: onCourseSessionChange } = useInput(
    notEmpty,
    "First"
  );

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

  const formStateIsValid =
    nameIsValid && universityIdIsValid && facultyIdIsValid && subjectIdIsValid;

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

  return (
    <Form method="POST" className={classes["create-academy-entity"]}>
      <ActionDataMessage successMessage="Pomyślnie utworzono przedmiot" />
      <EntityNameInput
        name="courseName"
        label="Nazwa przedmiotu"
        setValueIsValid={setNameIsValid}
      />
      <Select
        id="courseSession"
        name="courseSession"
        label="Wybierz semestr"
        options={courseSessionOptions}
        value={courseSession}
        onChange={onCourseSessionChange}
      />
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
      <div className={classes.actions}>
        <Button disabled={!formStateIsValid}>Stwórz przedmiot</Button>
      </div>
    </Form>
  );
};

export default CreateUniversityCourseForm;
