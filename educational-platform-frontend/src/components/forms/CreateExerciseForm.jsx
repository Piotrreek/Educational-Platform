import { useState } from "react";
import useInput from "../../hooks/useInput";
import { useNavigate } from "react-router-dom";

import Actions from "../auth/Actions";
import Button from "../ui/Button";
import Input from "../ui/Input";
import TextArea from "../ui/TextArea";

import { isPdf, notEmpty } from "../../utils/validators";
import { getToken } from "../../utils/jwtUtils";
import { BackendError } from "../../utils/errors";

import classes from "../ui/Form.module.css";

const CreateExcerciseForm = () => {
  const navigate = useNavigate();
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [error, setError] = useState();
  const [isSuccess, setIsSuccess] = useState(false);

  const {
    value: exerciseName,
    hasError: exerciseNameHasError,
    error: exerciseNameError,
    isValid: exerciseNameIsValid,
    onBlur: onExerciseNameBlur,
    onChange: onExerciseNameChange,
    reset: resetExerciseName,
  } = useInput(notEmpty);

  const {
    value: description,
    onChange: onDescriptionChange,
    reset: resetDescription,
  } = useInput(() => true);

  const {
    value: file,
    onChange: onFileChange,
    onBlur: onFileBlur,
    hasError: fileHasError,
    error: fileError,
    isValid: fileIsValid,
    reset: resetFile,
  } = useInput(isPdf, "", true);

  const onSubmit = async (e) => {
    e.preventDefault();

    const formData = new FormData();
    formData.append("name", exerciseName);
    formData.append("description", description);
    formData.append("exerciseFile", file);

    try {
      setIsSubmitting(true);
      const response = await fetch(
        `${process.env.REACT_APP_BACKEND_URL}exercise`,
        {
          method: "POST",
          body: formData,
          credentials: "include",
          headers: {
            Authorization: `Bearer ${getToken()}`,
          },
        }
      );

      if (response.status === 401) {
        navigate("/login");
      }

      if (!response.ok) {
        setError(BackendError);
        setIsSubmitting(false);

        setTimeout(() => {
          setError();
        }, 3000);

        return;
      }

      resetDescription();
      resetFile();
      resetExerciseName();
      setIsSuccess(true);
      setTimeout(() => {
        setIsSuccess(false);
      }, 2000);
    } catch (_) {
      setError(BackendError);
    }

    setIsSubmitting(false);
  };

  const formStateIsValid = exerciseNameIsValid && fileIsValid;

  return (
    <form className={classes.form} onSubmit={onSubmit}>
      {isSuccess && (
        <p className={classes.success}>Pomyślnie dodano ćwiczenie</p>
      )}
      {!!error && <p className={classes.error}>{error}</p>}
      <Input
        name="exercise-name"
        id="exercise-name"
        label="Nazwa ćwiczenia"
        type="text"
        value={exerciseName}
        error={exerciseNameError}
        hasError={exerciseNameHasError}
        onBlur={onExerciseNameBlur}
        onChange={onExerciseNameChange}
      />
      <TextArea
        id="description"
        name="description"
        label="Opis ćwiczenia"
        value={description}
        onChange={onDescriptionChange}
      />
      <Input
        id="file"
        name="file"
        type="file"
        label={`<p class="file-name">${
          !file ? "Dodaj plik PDF" : file.name
        }</p>`}
        onChange={onFileChange}
        onBlur={onFileBlur}
        hasError={fileHasError}
        error={fileError}
        hide={true}
      />
      <Actions className={classes["actions-right"]}>
        <Button disabled={!formStateIsValid || isSubmitting}>
          {isSubmitting ? "Dodaję" : "Dodaj"}
        </Button>
      </Actions>
    </form>
  );
};

export default CreateExcerciseForm;
