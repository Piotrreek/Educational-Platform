import { useState } from "react";
import { Form, useRouteLoaderData } from "react-router-dom";
import useInput from "../../../hooks/useInput";
import useCurrentAcademyEntitiesOptions from "../../../hooks/useCurrentAcademyEntitiesOptions";

import Select from "../../ui/Select";
import Button from "../../ui/Button";
import EntityNameInput from "./EntityNameInput";
import ActionDataMessage from "./ActionDataMessage";

import { notEmpty } from "../../../utils/validators";

import classes from "./CreateAcademyEntityForm.module.css";

const CreateFacultyForm = () => {
  const [nameIsValid, setNameIsValid] = useState(false);
  const loaderData = useRouteLoaderData("admin");
  const { universityOptions } = useCurrentAcademyEntitiesOptions(
    loaderData.universityEntities
  );

  const {
    value: universityId,
    onChange: onUniversityIdChange,
    onBlur: onUniversityIdBlur,
    hasError: universityIdHasError,
    error: universityIdError,
    isValid: universityIdIsValid,
  } = useInput(notEmpty);

  const formStateIsValid = nameIsValid && universityIdIsValid;

  return (
    <Form method="POST" className={classes["create-academy-entity"]}>
      <ActionDataMessage successMessage="Pomyślnie stworzono wydział" />
      <EntityNameInput
        name="facultyName"
        label="Nazwa wydziału"
        setValueIsValid={setNameIsValid}
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
      <div className={classes.actions}>
        <Button disabled={!formStateIsValid}>Stwórz wydział</Button>
      </div>
    </Form>
  );
};

export default CreateFacultyForm;
