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

const CreateUniversitySubjectForm = () => {
  const [nameIsValid, setNameIsValid] = useState(false);
  const loaderData = useRouteLoaderData("index");

  const { value: subjectDegree, onChange: onSubjectDegreeChange } = useInput(
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

  useAcademyEntitiesReset(null, null, resetFacultyId, universityId);

  const { universityOptions, facultyOptions } =
    useCurrentAcademyEntitiesOptions(
      loaderData.universityEntities,
      universityId
    );

  const formStateIsValid =
    nameIsValid && universityIdIsValid && facultyIdIsValid;

  const subjectDegreeOptions = [
    { value: "First", text: "Pierwszy stopień" },
    { value: "Second", text: "Drugi stopień" },
  ];

  return (
    <Form method="POST" className={classes["create-academy-entity"]}>
      <ActionDataMessage successMessage="Pomyślnie utworzono kierunek" />
      <EntityNameInput
        name="subjectName"
        label="Nazwa kierunku"
        setValueIsValid={setNameIsValid}
      />
      <Select
        id="subjectDegree"
        name="subjectDegree"
        label="Stopień studiów"
        options={subjectDegreeOptions}
        value={subjectDegree}
        onChange={onSubjectDegreeChange}
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
      <div className={classes.actions}>
        <Button disabled={!formStateIsValid}>Stwórz kierunek</Button>
      </div>
    </Form>
  );
};

export default CreateUniversitySubjectForm;
