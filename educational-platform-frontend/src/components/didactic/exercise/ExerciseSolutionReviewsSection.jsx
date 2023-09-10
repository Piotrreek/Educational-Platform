import { useState } from "react";

import MaterialModal from "../didacticMaterials/MaterialModal";

import classes from "./Exercise.module.css";
import NewSolutionReview from "./NewSolutionReview";

const ExerciseSolutionReviewsSection = ({
  solutionId,
  initialReviews,
  isLoggedIn,
}) => {
  const [reviews, setReviews] = useState(initialReviews);
  const [currentSelectedReview, setCurrentSelectedReview] = useState();

  return (
    <div className={classes.reviews}>
      <h3 className={classes["reviews-header"]}>Dyskusja</h3>
      {!reviews.length && <p>Brak komentarzy do rozwiązania</p>}
      {reviews.map((review) => (
        <div id={review.id} className={classes.review}>
          <p className={classes.solution__date}>
            Data dodania:
            <span>{new Date(review.createdOn).toLocaleDateString("pl-PL")}</span>
          </p>
          <p className={classes.solution__author}>
            Autor:
            <span>{review.author}</span>
          </p>
          {!!review.content && <p>{review.content}</p>}
          {review.hasFile && (
            <p className={classes.actions}>
              <a
                href={`${process.env.REACT_APP_BACKEND_URL}file/exercise/solution/review/${review.id}`}
              >
                Pobierz
              </a>
              <button onClick={() => setCurrentSelectedReview(review.id)}>
                Obejrzyj
              </button>
            </p>
          )}
        </div>
      ))}
      {isLoggedIn && (
        <NewSolutionReview solutionId={solutionId} setReviews={setReviews} />
      )}
      {!!currentSelectedReview && (
        <MaterialModal
          onClose={() => setCurrentSelectedReview()}
          files={[{ id: currentSelectedReview }]}
          initIndex={0}
          contentType="exercise/solution/review"
        />
      )}
    </div>
  );
};

export default ExerciseSolutionReviewsSection;
