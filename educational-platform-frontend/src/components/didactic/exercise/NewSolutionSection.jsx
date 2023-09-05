import { useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import useInput from "../../../hooks/useInput";

import FileInput from "../../ui/FileInput";
import Button from "../../ui/Button";

import { getToken } from "../../../utils/jwtUtils";
import { notEmpty } from "../../../utils/validators";
import { BackendError } from "../../../utils/errors";

import classes from "../../ui/Form.module.css";

const NewSolutionSection = ({ setSolutions }) => {
  const params = useParams();
  const navigate = useNavigate();
  const [error, setError] = useState("");
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

    try {
      const formData = new FormData();
      formData.append("solutionFile", file);

      const response = await fetch(
        `${process.env.REACT_APP_BACKEND_URL}exercise/${params.id}/solution`,
        {
          method: "POST",
          credentials: "include",
          body: formData,
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

        setTimeout(() => {
          setError("");
        }, 3000);
        return;
      }

      setIsSuccess(true);
      setTimeout(() => {
        setIsSuccess(false);
      }, 3000);

      resetFile();
      const data = await response.json();
      setSolutions(data);
    } catch (error) {
      console.log(error);
      setError(BackendError);

      setTimeout(() => {
        setError();
      }, 3000);
    }
  };

  return (
    <section className="content__section">
      <h2>Dodaj nowe rozwiązanie</h2>
      <form onSubmit={onSubmit}>
        {!!error && <p className={classes.error}>{error.error}</p>}
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
