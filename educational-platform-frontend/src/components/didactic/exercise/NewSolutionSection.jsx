import { useState } from "react";
import useInput from "../../../hooks/useInput";

import FileInput from "../../ui/FileInput";
import Button from "../../ui/Button";

import { notEmpty } from "../../../utils/validators";
import {
  addNewSolution,
  getSolutions,
} from "../../../api/exerciseSolutionsApi";

import classes from "../../ui/Form.module.css";

const NewSolutionSection = ({ setSolutions, exerciseId }) => {
  const [isSuccess, setIsSuccess] = useState(false);

  const {
    value: file,
    onChange: onFileChange,
    onBlur: onFileBlur,
    hasError: fileHasError,
    error: fileError,
    isValid: fileIsValid,
    reset: resetFile,
  } = useInput(notEmpty, "", true);

  const onSubmit = async (e) => {
    e.preventDefault();

    await addNewSolution(file, exerciseId);
    setSolutions(await getSolutions(exerciseId));

    resetFile();

    setIsSuccess(true);
    setTimeout(() => {
      setIsSuccess(false);
    }, 2000);
  };

  return (
    <section className="content__section">
      <h2>Dodaj nowe rozwiązanie</h2>
      <form onSubmit={onSubmit}>
        {isSuccess && (
          <p className={classes.success}>Pomyślnie dodano rozwiązanie</p>
        )}
        <FileInput
          id="file"
          name="file"
          onBlur={onFileBlur}
          onChange={onFileChange}
          label={`<p class="file-name">${
            !file ? "Dodaj plik z rozwiązaniem" : file.name
          }</p>`}
          hasError={fileHasError}
          error={fileError}
        />
        <Button disabled={!fileIsValid}>Dodaj</Button>
      </form>
    </section>
  );
};

export default NewSolutionSection;
