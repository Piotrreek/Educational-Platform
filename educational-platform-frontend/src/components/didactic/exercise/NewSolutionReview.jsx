import { useState } from "react";
import useInput from "../../../hooks/useInput";

import FileInput from "../../ui/FileInput";
import Button from "../../ui/Button";
import TextArea from "../../ui/TextArea";

import {
  addNewReview,
  getReviews,
} from "../../../api/exerciseSolutionReviewsApi";

import classes from "../../ui/Form.module.css";

const NewSolutionReview = ({ solutionId, setReviews }) => {
  const [isSuccess, setIsSuccess] = useState(false);

  const {
    value: file,
    onChange: onFileChange,
    reset: resetFile,
  } = useInput(() => true, "", true);

  const {
    value: content,
    onChange: onContentChange,
    reset: resetContent,
  } = useInput(() => false);

  const onSubmit = async (e) => {
    e.preventDefault();

    await addNewReview(file, content, solutionId);
    setReviews(await getReviews(solutionId));

    resetFile();
    resetContent();

    setIsSuccess(true);
    setTimeout(() => {
      setIsSuccess(false);
    }, 2000);
  };
  return (
    <>
      <h3 style={{ marginBottom: "10px" }}>Dodaj nowy komentarz</h3>
      <form onSubmit={onSubmit}>
        {isSuccess && (
          <p className={classes.success}>Pomyślnie dodano komentarz</p>
        )}
        <FileInput
          id="review-file"
          name="review-file"
          onChange={onFileChange}
          label={`<p class="file-name">${
            !file ? "Dodaj plik (opcjonalnie)" : file.name
          }</p>`}
        />
        <TextArea
          id="content"
          name="content"
          label="Treść"
          onChange={onContentChange}
          value={content}
        />
        <Button>Dodaj</Button>
      </form>
    </>
  );
};

export default NewSolutionReview;
