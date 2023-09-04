import FileInput from "../../ui/FileInput";

import useInput from "../../../hooks/useInput";
import { notEmpty } from "../../../utils/validators";
import Button from "../../ui/Button";

const NewSolutionSection = () => {
  const {
    value: file,
    onChange: onFileChange,
    onBlur: onFileBlur,
    hasError: fileHasError,
    error: fileError,
    isValid: fileIsValid,
  } = useInput(notEmpty, "", true);

  const onSubmit = (e) => {
    e.preventDefault();
  };

  return (
    <section className="content__section">
      <h2>Dodaj nowe rozwiązanie</h2>
      <form onSubmit={onSubmit}>
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
