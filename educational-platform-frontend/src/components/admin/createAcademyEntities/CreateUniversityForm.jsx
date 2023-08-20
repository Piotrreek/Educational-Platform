import { Form } from "react-router-dom";
import { useState } from "react";

import Button from "../../ui/Button";
import EntityNameInput from "./EntityNameInput";
import ActionDataMessage from "./ActionDataMessage";

import classes from "./CreateAcademyEntityForm.module.css";

const CreateUniversityForm = () => {
  const [nameIsValid, setNameIsValid] = useState(false);

  return (
    <Form method="POST" className={classes["create-academy-entity"]}>
      <ActionDataMessage successMessage="Pomyślnie stworzono uczelnię" />
      <EntityNameInput
        name="universityName"
        label="Nazwa uczelni"
        setValueIsValid={setNameIsValid}
      />
      <div className={classes.actions}>
        <Button disabled={!nameIsValid}>Stwórz uczelnię</Button>
      </div>
    </Form>
  );
};

export default CreateUniversityForm;
