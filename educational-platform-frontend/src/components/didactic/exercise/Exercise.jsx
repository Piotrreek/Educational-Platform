import { useState, useCallback } from "react";
import { useLoaderData, useParams } from "react-router-dom";
import useAuth from "../../../hooks/useAuth";

import DescriptionSection from "../common/DescriptionSection";
import OpinionsSection from "../common/OpinionsSection";
import RateMaterial from "../common/RateMaterial";
import RatingsSection from "../common/RatingsSection";
import SolutionsSection from "./SolutionsSection";
import MaterialModal from "../didacticMaterials/MaterialModal";

import pdf from "../../../assets/pdf.svg";

const Exercise = () => {
  const { ctx } = useAuth();
  const [exerciseModalOpened, setExerciseModalOpened] = useState(false);
  const params = useParams();
  const exercise = useLoaderData();

  const [ratingObject, setRatingObject] = useState({
    lastRatings: exercise.lastRatings,
    averageRating: exercise.averageRating,
  });

  const handleRateChange = useCallback((averageRating, lastRatings) => {
    setRatingObject({ lastRatings: lastRatings, averageRating: averageRating });
  }, []);

  return (
    <div className="content">
      <div className="content__header">
        <h2 className="content__name">{exercise.name}</h2>
        <div className="content__header-content">
          <div className="content__thumbnail">
            <img src={pdf} alt="thumbnail" />
          </div>
          <div className="content__actions">
            <a
              href={`${process.env.REACT_APP_BACKEND_URL}file/exercise/${params.id}`}
            >
              Pobierz
            </a>
            <button onClick={() => setExerciseModalOpened(true)}>
              Obejrzyj
            </button>
            <RateMaterial
              rate={exercise.usersRate}
              contentId={params.id}
              endPointPart="exercise"
              handleRateChange={handleRateChange}
            />
          </div>
        </div>
      </div>
      <div className="content__section content__description-section">
        <DescriptionSection description={exercise.description} />
      </div>
      <SolutionsSection
        solutionList={exercise.solutions}
        isLoggedIn={ctx.claims.isLoggedIn}
        exerciseId={params.id}
      />
      <RatingsSection
        ratings={ratingObject.lastRatings}
        averageRating={ratingObject.averageRating}
        noRatingsText={`To ćwiczenie nie posiada jeszcze żadnych ocen. Możesz je ocenić pod
        przyciskami "Pobierz" i "Obejrzyj" jeśli się zalogujesz.`}
      />
      <OpinionsSection
        opinionList={exercise.comments}
        noOpinionsText="To ćwiczenie nie posiada jeszcze żadnych opinii. Możesz dodać pod spodem opinię jeśli się zalogujesz"
        contentId={params.id}
        endpoint="exercise"
      />
      {exerciseModalOpened && (
        <MaterialModal
          onClose={() => setExerciseModalOpened(false)}
          files={[{ id: params.id }]}
          initIndex={0}
          contentType="exercise"
        />
      )}
    </div>
  );
};
export default Exercise;
